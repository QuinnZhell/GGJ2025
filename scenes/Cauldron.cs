using Godot;
using System;

public partial class Cauldron : Node2D
{
	stoke stoke;
	stir stir;
	AltSpit spit;
	BarMaster barMaster;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stoke = GetNode<Node2D>("Stoke Flames").GetNode<Sprite2D>("Sprite2D") as stoke;
		stir = GetNode<Node2D>("Stir Pot").GetNode<Node2D>("Stir Pot") as stir;
		spit = GetNode<Node2D>("AltSpit") as AltSpit;
        barMaster = GetNode<Node2D>("Potion").GetNode<BarMaster>("BarMaster");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		barMaster.StokeChange(stoke.Temperature/10);
        barMaster.StirChange(stir.StirringSpeed / 2);
		if (spit.spitting)
		{
            barMaster.SpitChange(32);
        }
		else
		{
            barMaster.SpitChange(0);
        }
    }
}
