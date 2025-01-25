using Godot;
using System;

public partial class Cauldron : Node2D
{
	stoke stoke;
	stir stir;
	BarMaster barMaster;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stoke = GetNode<Node2D>("stoke_flames").GetNode<Sprite2D>("Sprite2D") as stoke;
        stir = GetNode<Node2D>("stir_pot").GetNode<Sprite2D>("ladle") as stir;
        barMaster = GetNode<BarMaster>("BarMaster");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		barMaster.StokeChange(-(stoke.Temperature % 100) / 10 , stoke.Temperature / 10);
        barMaster.StirChange(-(stir.StirringSpeed), stir.StirringSpeed / 2);
    }
}
