using Godot;
using System;

public partial class stir : Sprite2D
{
	private float stirringRadius = 2f;
	private float stirringSpeed = 0f; // Speed tracker for debugging
	private float potRadius = 150.0f;
	private float ladleOffset = 20.0f; // Distance from the pot's edge to the ladle's circular path
	private float currentAngle = 0f;
	private float previousAngle = 0.0f;
	private Vector2 potPosition = new Vector2(549, 337); // Hard coded since I couldn't figure out how to reference the node :)

	public override void _Ready()
	{
		// Calculate the effective stirring radius
		stirringRadius = potRadius - ladleOffset;
	}

	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		UpdateLadlePosition(mousePosition);

		// Calculate angular velocity
		float deltaAngle = Mathf.Abs(currentAngle - previousAngle);

		if (deltaAngle > Mathf.Pi) // Correct for crossing the -pi to pi boundary
		{ 
			deltaAngle = Mathf.Tau - deltaAngle;
		}

		stirringSpeed = (float)(deltaAngle / delta); // Angular velocity in radians/second
		previousAngle = currentAngle;

		GD.Print(stirringSpeed);
	}

	private void UpdateLadlePosition(Vector2 mousePosition)
	{
		// Calculate the angle between the mouse and the pot center
		currentAngle = (mousePosition - potPosition).Angle();

		Vector2 ladlePosition = potPosition + new Vector2(
			Mathf.Cos(currentAngle) * stirringRadius,
			Mathf.Sin(currentAngle) * stirringRadius
		);

		GlobalPosition = ladlePosition;

		// Rotate the ladle to face toward the center
		Rotation = currentAngle + Mathf.Pi / 2; // Add Pi/2 to make it point correctly
	}
}