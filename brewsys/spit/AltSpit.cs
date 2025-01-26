using Godot;
using System;

public partial class AltSpit : Node2D
{
    public bool spitting = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _Process(double delta)
    {
        // If user presses space E, start spit QTE
        if (Input.IsActionJustPressed("spit"))
        {
            spitting = true;
        }

        if(Input.IsActionJustReleased("spit"))
        {
            spitting = false;
            GD.Print("Released");
        }
    }
}
