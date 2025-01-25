using Godot;
using System;
using System.Collections.Generic;

public partial class BarMaster : Node2D
{
    // Enum to represent the different bar types
    public enum BarType
    {
        Stability,
        Potency,
        Bubbliness
    }

    // Dictionary to store references to the bars
    private Dictionary<BarType, NeedyBar> bars;

    // Called when the node enters the scene tree for the first time
    public override void _Ready()
    {
        // Initialize the dictionary and populate it with the child nodes
        bars = new Dictionary<BarType, NeedyBar>
        {
            { BarType.Stability, GetNode<NeedyBar>("Stability") },
            { BarType.Potency, GetNode<NeedyBar>("Potency") },
            { BarType.Bubbliness, GetNode<NeedyBar>("Bubbliness") }
        };
    }

    // Set the goal properties for the specified bar type
    public void SetGoalProperties(float newGoal, float newLenience, BarType barType)
    {
        // Check if the bar exists in the dictionary and set the properties
        if (bars.ContainsKey(barType))
        {
            bars[barType].SetGoalProperties(newGoal, newLenience);
        }
        else
        {
            GD.PrintErr("Bar type not found: " + barType);
        }
    }

    public void SetNaturalRate(float newRate, BarType barType)
    {
        // Check if the bar exists in the dictionary and set the properties
        if (bars.ContainsKey(barType))
        {
            bars[barType].SetNewRate(newRate);
        }
        else
        {
            GD.PrintErr("Bar type not found: " + barType);
        }
    }

    public void SetFlatAddition(float newAddition, BarType barType)
    {
        // Check if the bar exists in the dictionary and set the properties
        if (bars.ContainsKey(barType))
        {
            bars[barType].SetFlatAddition(newAddition);
        }
        else
        {
            GD.PrintErr("Bar type not found: " + barType);
        }
    }

    public float CalculateOverallScore()
    {
        float StabilityScore = bars[BarType.Stability].Score;
        float PotencyScore = bars[BarType.Potency].Score;
        float BubblinessScore = bars[BarType.Bubbliness].Score;

        //Do something to calc overall score here..
        float overallScore = 0;

        return overallScore;

    }   
}
