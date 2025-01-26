using Godot;
using System;

public partial class SceneChange : MarginContainer
{

    private AudioStreamPlayer2D sfx;
    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer2D>("SFX");
    }

    private void _on_continue_pressed()
    {
        sfx.Play();
        GetTree().ChangeSceneToFile("res://scenes/brewing.tscn");
    }
}
