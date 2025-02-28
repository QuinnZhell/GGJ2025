using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

public static class GameMaster
{
    //Tweak these heuristically to balance request requirements and gold reward
    private static int SCORE_MULT_CONSTANT = 1000;
    private static int REWARD_MULT_CONSTANT = 10;
    public static int STARTING_GOLD = 100;
	public static int DAILY_QUOTA_MULT = 1;
	
	public static Potion potion;
	public static Request request;
	public static Inventory inventory = new Inventory();
    public static List<Request> requests = new List<Request>();
	private static Random rand = new Random();

    public static int day = 1;
    public static int dailyQuota = day * DAILY_QUOTA_MULT;
	public static int goldEarnedToday = 0;


    public static void generateRequests(int numRequests)
	{
        for (int i = 0; i < numRequests; i++)
		{
			
			requests.Add(new Request(calcMinScore(),calcGoldReward(),generateTasks(rand.Next(0,3))));
		}
		return;
		//Nested functions for generating requests
		int calcMinScore()
		{
			return (rand.Next(1, day)*SCORE_MULT_CONSTANT);
		}
		int calcGoldReward()
		{
			return (rand.Next(1, day) * REWARD_MULT_CONSTANT);
		}
		Task[] generateTasks(int numTasks)
		{
            Task[] tasks = new Task[numTasks];
			for (int i = 0; i < numRequests; i++)
			{
				tasks[i] = new Task(getRandomEnumValue<Task.taskType>(),rand.Next(1,3));
			}
			return tasks;
		}
	}
	//Get random value from generic enum
    static T getRandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(rand.Next(v.Length));
    }
    //Call when a request needs to start
    public static void startRequest(Request chosenRequest)
	{
		request = chosenRequest;
	}

	public static void earnGold(int gold)
	{
		inventory.setGold(inventory.getGold() + gold);
		goldEarnedToday += gold;
	}
	public static void newDay()
	{
		day++;
		goldEarnedToday = 0;
		dailyQuota = day * DAILY_QUOTA_MULT;
		requests.Clear();
	}
}
