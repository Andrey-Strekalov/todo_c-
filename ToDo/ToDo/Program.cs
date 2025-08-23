using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDo;

// Write a function to change the completed-status of the task, add a strikethrough style to the text of the completed task
// Make the output to console more comfortable for users

public class MainClass
{
    public static void Main()
    {

        List<MyTask> tasks = new List<MyTask>();

        tasks = LoadFromFile("data/TaskList.dat");

        int point = 1;
        do
        {
            point = MainMenu();

            switch (point)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    PrintList(tasks);
                    break;
                case 3:
                    RemoveTask(tasks);
                    break;
                case 4:
                    RemoveCompleted(tasks);
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("*  Такого пункта нет! Введите корректное значение.  *");
                    break;
            }

        }
        while (point != 5);
        SaveToFile(tasks, "data/TaskList.dat");


        int GetId()
        {
            int id = tasks.Count + 1;
            return id;
        }


        int MainMenu()
        {

            while (true)
            {

                Console.WriteLine("1 - добавить задачу  2 - просмотреть список 3 - Удалить задачу  4 - Удалить выполненные  5 - выход");
                Console.WriteLine("Выберите пункт: ");

                if (int.TryParse(Console.ReadLine(), out int point) && point >= 1 && point <= 5) return point;
                else Console.WriteLine("*  Введите корректное значение!  *");
            }
        }

        void AddTask()
        {
            Console.WriteLine("Что нужно сделать?");
            string content = Console.ReadLine();
            MyTask task = new MyTask(GetId(), content, false);
            tasks.Add(task);
            Console.WriteLine("*  Задача создана!  *");

            SaveToFile(tasks, "data/TaskList.dat");
        }

        static List<MyTask> LoadFromFile(string FilePath)
        {
            string[] data = File.ReadAllLines(FilePath);
            List<MyTask> tasks = new List<MyTask>();


            if (string.IsNullOrWhiteSpace(File.ReadAllText(FilePath)))
            {
                Console.WriteLine("data file is empty");
                return tasks;


            }
            Console.WriteLine("Is not must to be");



            foreach (string line in data)
            {

                if (string.IsNullOrWhiteSpace(line)) { continue; }
                string[] CurrentData = line.Split("|");

                MyTask CurrentTask = new MyTask(

                    int.Parse(CurrentData[0].Trim()),
                    CurrentData[1].Trim(),
                    bool.Parse(CurrentData[2].Trim().ToLower())
                );
                tasks.Add(CurrentTask);
            }

            return tasks;
        }

        void SaveToFile(List<MyTask> tasks, string FilePath)
        {

            var lines = tasks.Select(task => $"{task.Id}|{task.TaskContent}|{task.Comleted}");
            File.WriteAllLines(FilePath, lines);

        }

        void PrintList(List<MyTask> tasks)
        {
            foreach (MyTask task in tasks)
            {
                task.printTask();
            }
        }

        void RemoveTask(List<MyTask> tasks)
        {
            Console.Write("Введите ID задачи для удаления: ");
            var TargetId = int.Parse(Console.ReadLine());
            MyTask FoundedTask = tasks.Find(task => task.Id == TargetId);
            string TaskContent = "";

            if (FoundedTask != null)
            {
                TaskContent = FoundedTask.TaskContent;
                Console.Write($"Удалить задачу *{TaskContent}* ? (y/n)");
                string temp = Console.ReadLine();
                if (temp == "y")
                {

                    tasks.RemoveAll(task => task.Id == TargetId);
                }
            }
            else
            {
                Console.WriteLine($"* Ошибка! Задача под номером {TargetId} не найдена!");
            }



            UpdateId(tasks);
        }

        void RemoveCompleted(List<MyTask> tasks)
        {
            tasks.RemoveAll(task => task.Comleted == true);
            UpdateId(tasks);
        }

        void UpdateId(List<MyTask> tasks)
        {

            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].Id = i + 1;
            }

        }


    }
}
