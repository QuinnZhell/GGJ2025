using Godot;
using System;

public partial class AltSpit : Node2D
{
    public bool spitting = false;
    private AudioStreamPlayer2D audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        audio = GetNode<AudioStreamPlayer2D>("Puke");
	}

    public override void _Process(double delta)
    {
        // If user presses space E, start spit QTE
        if (Input.IsActionJustPressed("spit"))
        {
            spitting = true;
            audio.Play();
        }

        if(Input.IsActionJustReleased("spit"))
        {
            spitting = false;
            audio.Stop();
        }
    }
}
