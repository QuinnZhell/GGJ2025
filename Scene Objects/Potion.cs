using Godot;
using System;

public partial class Potion : Node2D
{
	BarMaster barMaster;
	RichTextLabel score;
	Timer timer;

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

	private void _on_timer_timeout()
	{
		timeEnded = true;
	}
}
