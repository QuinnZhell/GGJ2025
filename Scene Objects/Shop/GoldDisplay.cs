using Godot;
using System;

public partial class GoldDisplay : RichTextLabel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = GameMaster.inventory.GetCoins().ToString();
	}

	public void UpdateGold()
	{
        Text = GameMaster.inventory.GetCoins().ToString();
    }
}
