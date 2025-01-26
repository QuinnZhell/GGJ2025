using Godot;
using System;

public partial class Request : Node2D
{
	private static int BASE_GOLD_REWARD = 1;
	private static int BASE_VALUE_REQUIREMENT = 1;

	private int minScore = 0;
	private int goldReward = 0;
	private bool fulfilled = false;
	private Potion potion;
	private Task[] tasks;

	public Request(int score, int reward)
	{
		setMinScore(score*GameMaster.day);
		setGoldReward(reward);
		potion = GameMaster.potion;
	}
    public Request(int score, int reward, Task[] tasks)
    {
        setMinScore(score);
        setGoldReward(reward);
		potion = GameMaster.potion;
        setTasks(tasks);
    }

    //Tweak heuristically later for balancing
    public void setMinScore(int score)
	{
		minScore = (minScore*BASE_VALUE_REQUIREMENT);
	}
	public void setGoldReward(int reward)
	{
		goldReward = (goldReward * BASE_GOLD_REWARD);
	}
	public void setTasks(Task[] tasks)
	{
		this.tasks = tasks;
	}
	public int getMinScore()
	{
		return minScore;
	}
	public int getGoldReward()
	{
		return goldReward;
	}
	public Task[] getTasks()
	{
		return tasks;
	}

    public bool complete()
    {
        //Check if all tasks have been completed
        foreach (Task task in getTasks())
        {
            if (task.completed() == false)
            {
                return false;
            }
        }
        //Then check if default requirement (potion score) has been met
		if (potion.getScore() < minScore)
		{
			return false;
		}
		else
		{
			fulfilled = true;
		}
        return fulfilled;
    }
	//Call at end of timer
	public bool end()
	{
		if (complete())
		{
			GameMaster.earnGold(goldReward);
		}
		return fulfilled;
	}
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
