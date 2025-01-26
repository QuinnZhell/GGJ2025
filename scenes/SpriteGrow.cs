using Godot;
using System;
using System.Drawing;

public partial class SpriteGrow : Node2D
{
    [Export] public bool StopGrowing = false; // Growth stops when this is true
    [Export] public float GrowthRate = 0.1f;  // Adjust growth speed
    [Export] public float MaxSize = 3.0f;     // Maximum size before triggering the function
    public Potion potion;
    private AudioStreamPlayer2D audio;
    private Timer timer;
    bool timerStarted = false;

    public override void _Ready()
    {
        potion = GetParent().GetNode<Potion>("Potion");
        audio = GetNode<AudioStreamPlayer2D>("audio");
        timer = GetNode<Timer>("Timer");
    }

    public override void _Process(double delta)
    {
        StopGrowing = !potion.failing;
       
        if (!StopGrowing)
        {
            // Grow the sprite
            float scaleIncrease = (float)delta * GrowthRate;
            Scale += new Vector2(scaleIncrease, scaleIncrease);
        }

        // Check if the sprite has exceeded the max size
        if (Scale.X >= MaxSize || Scale.Y >= MaxSize)
        {
            TriggerFunction();
        }
    }

    // Function to trigger when the size is exceeded
    private void TriggerFunction()
    {
        audio.Play();
        Modulate = new Godot.Color(0.5f, 0.5f, 0.5f); // Gray color
        if (!timerStarted)
        {
            timer.Start();
            timerStarted = true;
        }
        // You can place additional logic here
        // For example, calling a custom function, changing behavior, etc.
    }

    private void _on_timer_timeout()
    {
        GetTree().ChangeSceneToFile("res://scenes/Main Menu.tscn");
    }
}
