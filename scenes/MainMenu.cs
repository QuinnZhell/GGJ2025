using Godot;
using System;

public partial class MainMenu : Control
{
    private AudioStreamPlayer2D sfx;
    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer2D>("SFX");
    }
    private void _on_play_pressed()
    {
        sfx.Play();
        GetTree().ChangeSceneToFile("res://Scene Objects/Shop/Shop.tscn");
    }

    private void _on_quit_pressed()
    {
        sfx.Play();
        GetTree().Quit();
    }

}
