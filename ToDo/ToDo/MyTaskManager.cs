using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo;

public static class MyTaskManager
{
    private static List<MyTask> tasks = new List<MyTask>();
    private static string FilePath = "data/TaskList.dat";

    static MyTaskManager()
    {
      LoadFromFile(FilePath);

    }

    public static int GetId()
    {
        int id = tasks.Count + 1;
        return id;
    }

    public static void AddTask()
    {
        Console.Write("Нужно сделать: ");
        string content = Console.ReadLine();
        MyTask task = new MyTask(GetId(), content, false);
        tasks.Add(task);
        Console.WriteLine("*  Задача создана!  *");
        SaveToFile(FilePath);
        Console.WriteLine(" ");
    }

    private static void LoadFromFile(string FilePath)
    {
        string[] data = File.ReadAllLines(FilePath);
        tasks = new List<MyTask>();

        if (string.IsNullOrWhiteSpace(File.ReadAllText(FilePath)))
        {
            Console.WriteLine("data file is empty");
            return;
        }

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
    }

    public static void SaveToFile(string FilePath)
    {
        var lines = tasks.Select(task => $"{task.Id}|{task.TaskContent}|{task.Comleted}");
        File.WriteAllLines(FilePath, lines);
    }

    public static void PrintList()
    {
        foreach (MyTask task in tasks)
        {
            task.printTask();
        }
        Console.WriteLine("");

    }

    public static void RemoveTask()
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
        Console.WriteLine("");
    }

    public static void RemoveCompleted()
    {
        tasks.RemoveAll(task => task.Comleted == true);
        UpdateId(tasks);
        Console.WriteLine("");
    }

    public static void ChangeStatus()
    {
        Console.Write("Введите ID задачи для смены статуса: ");
        var TargetId = int.Parse(Console.ReadLine());
        MyTask FoundedTask = tasks.Find(task => task.Id == TargetId);
        string TaskContent = FoundedTask.TaskContent;

        Console.Write($"{TaskContent} - Сменить статус? (y/n) - ");
        string temp = Console.ReadLine();
        if (temp == "y")
        {
            FoundedTask.completed = !FoundedTask.completed;
            Console.WriteLine($"Задача * {TaskContent} *  -  {(FoundedTask.completed ? "выполнена " : "не выполнена")}");
        }

        SaveToFile(FilePath);
        Console.WriteLine("");

    }

    public static void UpdateId(List<MyTask> tasks)
    {

        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }

    }

}
