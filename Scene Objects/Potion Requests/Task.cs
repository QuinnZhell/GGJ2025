using Godot;
using System;

public partial class Task : Node
{
    public enum taskType
    {
        requiredAttribute,
        requiredIngredient,
        bannedIngredient
    }

	private taskType requirement;
	private int requiredValue;
	//TODO: Another variable with ingredient/attribute type
	private Ingredient ingredient;
	private bool potionFailed = false;
	
	public Task(taskType task, int value)
	{
		setRequirement(task);
		setRequiredValue(value);
	}
    public Task(taskType task, int value, Ingredient ingredient)
    {
        setRequirement(task);
        setRequiredValue(value);
		setIngredient(ingredient);
    }
    private void setRequirement(taskType task)
	{
		this.requirement = task;
	}
	private void setRequiredValue(int value)
	{
		this.requiredValue = value;
	}
	private void setIngredient(Ingredient ingredient)
	{
		this.ingredient = ingredient;
	}
	public bool completed()
	{
		//Compare current potion contents against required value
		switch(requirement)
		{
			case taskType.requiredAttribute:
				break;
			case taskType.requiredIngredient:
				break;
			case taskType.bannedIngredient:
				break;
		}
		return true;
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
