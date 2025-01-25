using Godot;
using System;

public partial class MainMenu : Control
{
    private void _on_play_pressed()
    {
        GetTree().ChangeSceneToFile("res://game.tscn");
    }

    private void _on_options_pressed()
    {
        GetTree().ChangeSceneToFile("res://options.tscn");
    }

    private void _on_quit_pressed()
    {
        GetTree().Quit();
    }

}
