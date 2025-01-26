using Godot;
using System;

public partial class ScrollRequest : Control
{
    private VBoxContainer currentTextContainer;

    public override void _Ready()
    {
        // Find the VBoxContainer inside the ScrollContainer
        currentTextContainer = GetNode<VBoxContainer>("VBoxContainer");

        // Add multiple text objects
        for (int i = 0; i < 10; i++)
        {
            AddTextToBox($"Message {i + 1}");
        }
    }

    private void AddTextToBox(string text)
    {
        Label messageLabel = new Label();
        messageLabel.Text = text;
        messageLabel.AddThemeFontSizeOverride("font_size", 24); // Set font size

        currentTextContainer.AddChild(messageLabel); // Add label to VBoxContainer
    }
}