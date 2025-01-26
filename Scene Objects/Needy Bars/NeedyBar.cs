using Godot;
using System;

public partial class NeedyBar : Control
{
    [Export] public float NaturalRate = 2f; // Passive change per second
    private float ActualRate = 0f;
    [Export] public float BarSpeed = 0.1f;   // Base speed at which ActualValue reaches TargetValue
    [Export] public float GoalValue = 70f;   // Ideal value for max score
    [Export] public float GoalLenience = 10f;// Acceptable range for scoring

    public float FlatAddition = 0f;  // Flat add/subtract applied to Target
    private float TargetValue = 50f; // Desired level
    private float ActualValue = 50f; // Current level
    private float MinValue = 0f;
    private float MaxValue = 100f;
    public float Score = 100f;
    public bool failing = false;
    public float StokeMod = 0f;
    public float StirMod = 0f;
    public float SpitMod = 0f;

    public bool failure = false;

    private ProgressBar _progressBar;
    private ColorRect _colorRect;
    private ColorRect _goalRect; // For goal visualization
    private ColorRect _lenienceRect; // For lenience range visualization

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>("ProgressBar");
        _colorRect = GetNode<ColorRect>("ColorRect");

        // Goal and Lenience Visualization
        _goalRect = new ColorRect();
        _goalRect.Size = new Vector2(10, _progressBar.Size.Y); // Goal width
        _goalRect.Modulate = new Color(1.0f, 0.843f, 0.0f, 0.5f); // Gold color with 50% transparency
        _goalRect.ZIndex = 1; // Ensure the goal rect is on top
        AddChild(_goalRect);

        _lenienceRect = new ColorRect();
        _lenienceRect.Size = new Vector2(10, _progressBar.Size.Y); // Lenience width 
        _lenienceRect.Modulate = new Color(0.3f, 1f, 0.3f, 0.5f); // Lenience color (light green) with 50% transparency
        _lenienceRect.ZIndex = 0; // Ensure lenience rect is behind the goal rect
        AddChild(_lenienceRect);
    }

    public override void _Process(double delta)
    {

        //Update Natural Rate
        ActualRate = NaturalRate + StokeMod + SpitMod + StirMod;

        // Apply natural rate change
        TargetValue += ActualRate * (float)delta;

        // Apply flat addition over time
        if (FlatAddition != 0)
        {
            TargetValue += FlatAddition;
            FlatAddition = 0;
        }

        // Adjust BarSpeed dynamically with a cap on the maximum speed increase
        float distanceToTarget = Mathf.Abs(TargetValue - ActualValue);

        // Define maximum speed for the bar
        float maxSpeed = 1.0f;  // Max speed adjustment (lower values = slower movement)
        float minSpeed = 0.5f; // Minimum speed to prevent instant movement

        // Calculate the speed based on distance. The closer the target, the slower the movement.
        float speed = Mathf.Lerp(maxSpeed, minSpeed, Mathf.Clamp(distanceToTarget / 50f, 0f, 1f));

        // For very small distances, make the speed even smaller for smoothness
        if (distanceToTarget < 0.01f)
        {
            speed = Mathf.Lerp(speed, 0f, 0.2f); // Gradually slow down for tiny distances
        }

        // Gradually update ActualValue towards TargetValue with this calculated speed
        if (distanceToTarget > speed) // Continue moving towards the target only if distance is greater than speed
        {
            if (ActualValue < TargetValue)
            {
                ActualValue += speed; // Move towards the target (if the current value is less than the target)
            }
            else if (ActualValue > TargetValue)
            {
                ActualValue -= speed; // Move towards the target (if the current value is greater than the target)
            }
        }
        else
        {
            // Once we're within a small threshold of the target, set ActualValue directly to TargetValue
            ActualValue = TargetValue;
        }

        // Clamp within min/max range
        ActualValue = Mathf.Clamp(ActualValue, MinValue, MaxValue);
        TargetValue = Mathf.Clamp(TargetValue, MinValue, MaxValue);
        Score = Mathf.Clamp(Score, 0f, 100f);

        // Update UI
        _progressBar.Value = ActualValue;
        UpdateColor();

        // Ensure the ColorRect width matches the fill amount of the ProgressBar
        float fillRatio = (ActualValue - MinValue) / (MaxValue - MinValue);
        _colorRect.Size = new Vector2(_progressBar.Size.X * fillRatio, _colorRect.Size.Y);

        // Score calculation based on proximity to goal (light green and gold areas)
        CalculateScore();

        // Check explosion condition
        if (ActualValue <= MinValue || ActualValue >= MaxValue)
        {
            failure = true;
        }
    }

    private void CalculateScore()
    {
        // Use the same logic from UpdateColor to calculate if we are in the gold or light green area
        float distance = Mathf.Abs(ActualValue - GoalValue);

        // Color based on proximity to goal
        if (distance < GoalLenience * 0.5f)
        {
            Score += 1 * (float)GetProcessDeltaTime(); // Double score for gold area
            failing = false;
        }
        else if (distance > GoalLenience)
        {
            Score -= 3 * (float)GetProcessDeltaTime(); // Regular score for light green area
            failing = true;
        }
        else
        {
            failing = false;
        }

    }

    private void UpdateColor()
    {
        float distance = Mathf.Abs(ActualValue - GoalValue);

        // Goal position: Position the goal rectangle based on GoalValue
        float goalPosition = (GoalValue - MinValue) / (MaxValue - MinValue) * _progressBar.Size.X;
        _goalRect.Position = new Vector2(goalPosition - _goalRect.Size.X / 2, _progressBar.Position.Y);

        // Lenience range: Show an acceptable range around the goal
        float lenienceStart = (GoalValue - GoalLenience - MinValue) / (MaxValue - MinValue) * _progressBar.Size.X;
        float lenienceEnd = (GoalValue + GoalLenience - MinValue) / (MaxValue - MinValue) * _progressBar.Size.X;
        _lenienceRect.Position = new Vector2(lenienceStart, _progressBar.Position.Y);
        _lenienceRect.Size = new Vector2(lenienceEnd - lenienceStart, _progressBar.Size.Y);

        // Color based on proximity to goal
        if (distance < GoalLenience * 0.5f)
            _colorRect.Modulate = new Color(1.0f, 0.843f, 0.0f); // Gold
        else if (distance < GoalLenience)
            _colorRect.Modulate = new Color(0.3f, 1f, 0.3f); // Light green
        else if (ActualValue <= MinValue || ActualValue >= MaxValue)
            _colorRect.Modulate = new Color(0.5f, 0f, 0f); // Dark Red
        else if (ActualValue <= MinValue + 5 || ActualValue >= MaxValue - 5)
            _colorRect.Modulate = new Color(1f, 0f, 0f); // Red
        else
            _colorRect.Modulate = new Color(1f, 1f, 1f); // White 
    }

    public void SetGoalProperties(float newGoal, float newLenience)
    {
        // Update the bar properties
        GoalValue = newGoal;
        GoalLenience = newLenience;

        // Update the visual markers
        UpdateColor();
    }
    public void SetNewRate(float newRate)
    {
        NaturalRate = newRate;
    }

    public void AddToRate(float rateAdd)
    {
        NaturalRate += rateAdd;
    }

    public void SetFlatAddition(float newAddition)
    {
        FlatAddition = newAddition;
    }

    public void Reset()
    {
        NaturalRate = 2f;
        ActualValue = 50f;
        Score = 0;
        BarSpeed = 0.1f;
        Score = 100f;
    }
}
