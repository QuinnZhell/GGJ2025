using Godot;
using System;

public partial class SceneChange : MarginContainer
{
    private void _on_continue_pressed()
    {
        GetTree().ChangeSceneToFile("res://game.tscn");
    }
}
