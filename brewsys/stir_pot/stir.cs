using Godot;
using System;

public partial class stir : Node2D
{
	private float _stirringSpeed = 0f; // Speed tracker for debugging
	public float StirringSpeed => _stirringSpeed;
	private float currentAngle = 0f;
	private float previousAngle = 0.0f;

	public Sprite2D cauldron;
	private Vector2 potPosition = new Vector2(662, 558); // Hard coded since I couldn't figure out how to reference the node :)

	private Vector2 startingPositionOffset = new Vector2(10, 10); // Offset for hand to ladle

	// Radius X Y of the cauldron
	private float radiusX = 200.0f; // X radius of the cauldron
	private float radiusY = 90.0f; // Y radius of the cauldron
	private bool isStirring = false; // Track if stirring is active

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		UpdateLadlePosition(mousePosition);

		// Calculate angular velocity
		float deltaAngle = Mathf.Abs(currentAngle - previousAngle);

		_stirringSpeed = (float)(deltaAngle / delta); // Angular velocity in radians/second
		previousAngle = currentAngle;

		//GD.Print(_stirringSpeed);
	}

	private void UpdateLadlePosition(Vector2 mousePosition)
	{

		Vector2 direction = mousePosition - potPosition;

		// If the point is outside the ellipse (i.e., outside of the normalised bounds of 1 in both x and y)
		float ellipseEquation = (float)(Math.Pow(direction.X, 2) / Math.Pow(radiusX, 2) + Math.Pow(direction.Y, 2) / Math.Pow(radiusY, 2));

		if (ellipseEquation <= 1 && Input.IsMouseButtonPressed(MouseButton.Left))
		{
			isStirring = true;
		}

		if (isStirring)
		{
			if (ellipseEquation > 1)
			{
				direction = direction.Normalized(); // Normalise the direction to ensure it's inside the ellipse
				// Scale the direction back by the ellipse's radii
				direction.X *= radiusX;
				direction.Y *= radiusY;

				// Set mouse position if it wonders outside the boundaries of the cauldron
				Input.SetMouseMode(Input.MouseModeEnum.Hidden); // Hide mouse
				Input.WarpMouse(potPosition + direction); // Move the mouse to the constrained position
			}
			GlobalPosition = potPosition + direction;
		}
		else 
		{
			GlobalPosition = potPosition + direction;
		}

		if (!Input.IsMouseButtonPressed(MouseButton.Left))
		{
			isStirring = false;
		}
		currentAngle = direction.Angle();  // Get the angle from the ladle to the mouse
	}
}
