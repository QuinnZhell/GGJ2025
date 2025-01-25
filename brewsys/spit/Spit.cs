using Godot;
using System;
using System.Threading.Tasks;

public partial class Spit : Node2D
{
	private Timer qteTimer;
	private bool qteStarted = false;
	private bool qteSuccess = false;

	// Time limit for the quick time event (in seconds)
	private float qteTimeLimit = 2.0f;

	public override void _Ready()
	{
		// Create and configure the timer
		qteTimer = new Timer();
		qteTimer.WaitTime = qteTimeLimit;
		qteTimer.OneShot = true;
		qteTimer.Connect("timeout", Callable.From(OnQteTimeout));
		AddChild(qteTimer);
	}

	public async Task WaitForQuarterSecond()
	{
		await Task.Delay(20);
	}

	// Start the QTE when called
	public async void StartQTE()
	{
		if (!qteStarted)
		{
			qteStarted = true;
			qteSuccess = false;

			// Start the timer
			qteTimer.Start();

            await WaitForQuarterSecond();

            // Show some visual feedback (e.g., prompt or UI indicator)
            GD.Print("Quick Time Event Started! Press 'Space' to pour.");
		}
	}

	public override void _Process(double delta)
	{
		// If user presses space E, start spit QTE
		if (!qteStarted && Input.IsActionJustPressed("spit")) 
		{
			StartQTE();
		}

		// Check if the player pressed the right key (e.g., "Space") during the QTE
		if (qteStarted && Input.IsActionJustPressed("spit")) // "ui_accept" is typically mapped to 'Space' key
		{
			qteSuccess = true;
			qteTimer.Stop(); // Stop the timer as the player succeeded
			HandleQteResult();
		}
	}

	private void OnQteTimeout()
	{
		// Timeout logic (failed QTE)
		if (!qteSuccess)
		{
			GD.Print("Failed! You didn't press the button in time.");
			HandleQteResult();
		}
	}

	private void HandleQteResult()
	{
		if (qteSuccess)
		{
			GD.Print("Success! You poured the water into the pot.");
			// Add water pouring logic here (e.g., trigger animation, sound effect)
		}
		else
		{
			GD.Print("Failure! You missed the timing.");
			// Handle failure (e.g., reset the state, play failure animation, etc.)
		}

		// Reset QTE state for the next time
		qteStarted = false;
	}
}
