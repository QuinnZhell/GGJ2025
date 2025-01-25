using Godot;
using System;

public partial class stoke : Sprite2D
{
	private bool isOn = false; // Tracks the current state of fire
	private AudioStreamPlayer2D audioPlayer;
	
	public override void _Ready()
	{
		// Get the AudioStreamPlayer node
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}
	
	public override void _Process(double delta)
	{
		// Check if the "ui_accept" key is pressed
		if (Input.IsActionJustPressed("ui_accept"))
		{
			ToggleColor();
		}
	}

	private void ToggleColor()
	{
		if (isOn)
		{
			Modulate = new Color(1, 1, 1); // Change to white
			isOn = false;
			
			// Stop the sound
			if (audioPlayer.Playing)
			{
				audioPlayer.Stop();
			}
		}
		else
		{
			Modulate = new Color(1, 0, 0); // Change to red
			isOn = true;
			
			// Play the sound
			if (!audioPlayer.Playing)
			{
				audioPlayer.Play(0);
			}
		}
	}
}
