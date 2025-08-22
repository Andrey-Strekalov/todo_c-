using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo;

public class MyTask
{
    private int id;
    private bool comleted;
    private string taskContent;

    public MyTask(int id, string taskContent, bool completed)
    {

        this.id = id;
        this.taskContent = taskContent;
        this.comleted = completed;
    }

    public int Id
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public bool Comleted
    {
        get { return this.comleted; }
        set { this.comleted = value; }
    }

    public string TaskContent
    {
        get { return this.taskContent; }
        set { this.taskContent = value; }
    }

    public void printTask()
    {
        Console.WriteLine($"#{this.id}    {this.taskContent}    {this.comleted}");
    }
}
