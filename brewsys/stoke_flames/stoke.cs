using Godot;
using System;

public partial class stoke : Sprite2D
{
	private bool isOn = false; // Tracks the current state of fire
	private AudioStreamPlayer2D audioPlayer;
	private float temperature = 0f;
	private const float maxTemperature = 100.0f; // Maximum stove temperature
	private const float minTemperature = 15.0f; // Minimum stove temperature
	public float Temperature => temperature;

	public override void _Ready()
	{
		// Get the AudioStreamPlayer node
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public override void _Process(double delta)
	{
		ChangeColour();
		// Check if the "ui_accept" key is pressed
		if (Input.IsActionJustPressed("ui_accept"))
		{
			ToggleColor();
		}

		if (isOn && temperature < maxTemperature)
		{
			temperature += (float)(10.0f * delta); // Increase temperature (rate of 10 units per second)
		}
		else if (!isOn && temperature > minTemperature)
		{
			temperature -= (float)(10.0f * delta); // Cool down when the stove is off
		}

		temperature = Mathf.Clamp(temperature, minTemperature, maxTemperature); // Ensure within limits
	}

	private void ToggleColor()
	{

		if (isOn)
		{
			isOn = false;
			
			// Stop the sound
			if (audioPlayer.Playing)
			{
				audioPlayer.Stop();
			}
		}
		else
		{
			isOn = true;
			// Play the sound
			if (!audioPlayer.Playing)
			{
				audioPlayer.Play(0);
			}
		}

	}
	
	private void ChangeColour()
	{
		// Adjust color based on temperature
		float t = temperature / maxTemperature; // Normalize temperature to a 0-1 range
		Modulate = new Color(1, 1 - t, 1 - t); // Change color from white to red as it heats

		//GD.Print("Stove is " + (isOn ? "On" : "Off") + " at " + temperature + " degrees");
	}
}
