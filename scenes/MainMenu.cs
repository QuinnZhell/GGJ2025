using Godot;
using System;

public partial class MainMenu : Control
{
    private void _on_play_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scene Objects/Shop/Shop.tscn");
    }

    private void _on_quit_pressed()
    {
        GetTree().Quit();
    }

}
