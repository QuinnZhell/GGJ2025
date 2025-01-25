using Godot;
using System;

public partial class Potion : Node2D
{
	BarMaster barMaster;
	RichTextLabel score;
	Timer timer;
	public Godot.Collections.Array<Ingredient> _ingredientArray;

    bool timeEnded = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		barMaster = GetNode<BarMaster>("BarMaster");
		score = GetNode<RichTextLabel>("Score");
		timer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!timeEnded)
		{
			score.Text = barMaster.CalculateOverallScore().ToString();
		}
	}

	private void AddIngredient(Ingredient ing)
	{
		_ingredientArray.Add(ing);
		barMaster.AddNaturalRate(ing.properties.baseStats.X, BarMaster.BarType.Stability);
        barMaster.AddNaturalRate(ing.properties.impulseStats.X, BarMaster.BarType.Stability);
        barMaster.AddNaturalRate(ing.properties.dynamicStats.X, BarMaster.BarType.Stability);
        barMaster.AddNaturalRate(ing.properties.baseStats.Y, BarMaster.BarType.Potency);
        barMaster.AddNaturalRate(ing.properties.impulseStats.Y, BarMaster.BarType.Potency);
        barMaster.AddNaturalRate(ing.properties.dynamicStats.Y, BarMaster.BarType.Potency);
        barMaster.AddNaturalRate(ing.properties.baseStats.Z, BarMaster.BarType.Bubbliness);
        barMaster.AddNaturalRate(ing.properties.impulseStats.Z, BarMaster.BarType.Bubbliness);
        barMaster.AddNaturalRate(ing.properties.dynamicStats.Z, BarMaster.BarType.Bubbliness);
    }

	private void _on_timer_timeout()
	{
		timeEnded = true;
	}
}
