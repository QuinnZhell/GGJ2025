using Godot;
using System;
using System.Transactions;

public partial class HarderRequest : Request
{
    public HarderRequest(int score, int reward, Task[] tasks):base(score,reward)
    {
        setTasks(tasks);
    }

    public new bool complete()
    {
        //Check if all tasks have been completed
        foreach (Task task in getTasks()){
            if(task.completed() == false)
            {
                return false;
            }
        }
        //Then check if default requirement (potion score) has been met
        return base.complete();
    }
}
