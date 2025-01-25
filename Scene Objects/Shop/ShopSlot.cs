using Godot;
using System;
using System.Drawing;

public partial class ShopSlot : Button
{

	//Get reference to players inv here..

	//Temp int for cash till we get inv, change to reference to players current cash
	int money = 100; 
	int IngredientPrice;
    int FinalPrice;
    [Export] private ingredientProperties _ingredient;
    private PopupPanel _popUp;

    // Duration for which the message will be shown
    private const float messageDuration = 1.0f;

    // Time for the animation
    private const float animationDuration = 0.5f;

    public override void _Ready()
    {
        _popUp = GetNode<PopupPanel>("ItemInfo");
        Control _control = _popUp.GetNode<Control>("Control");
        RichTextLabel _itemName = _control.GetNode<RichTextLabel>("Item Name");
        RichTextLabel _price = _control.GetNode<RichTextLabel>("Price");
        RichTextLabel _desc = _control.GetNode<RichTextLabel>("Desc");

        UpdatePopupPosition();

        int SalePercentage = 0;
        int x = Convert.ToInt32(GD.Randi() % 49);

        if (x >= 1 && x <= 3)
        {
            SalePercentage = x * 10;
        }

        FinalPrice = _ingredient.BasePrice * (100 - SalePercentage) / 100;

        _itemName.Text = _ingredient.Name;
        _desc.Text = _ingredient.Description;

        if(SalePercentage != 0)
        {
            _price.BbcodeEnabled = true;
            _price.Text = "Cost: " + "[color=#FFD700]" + FinalPrice.ToString() + "[/color]";
        }
        else
        {
            _price.Text = "Cost: " + FinalPrice.ToString();
        }

        //Signals for mouse entering and exiting
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        // Connect the 'pressed' signal to the method handling the button click
        Pressed += OnButtonPressed;
    }

    private void OnMouseEntered()
    {
        _popUp.Visible = true;
    }

    private void OnMouseExited()
    {
        _popUp.Visible = false;
    }

    // This method runs when the button is clicked
    private void OnButtonPressed()
    {
        money = TryToBuy(money);
    }

    private int TryToBuy(int cash)
    {
        if (money > FinalPrice)
        {
            money -= FinalPrice;
            ShowMessage("Purchased", true);
            //Add 1 of item to inventory here..
        }
        else
        {
            ShowMessage("Not Enough!", false);
        }

        return money;
    }

    private void UpdatePopupPosition()
    {
        if (_popUp != null)
        {
            // Make the popup position relative to the button.
            _popUp.Position = new Vector2I((int)Position.X + 200, (int)Position.Y - 25);  // Convert to Vector2I
        }
    }
    private async void ShowMessage(string text, bool isPurchased)
    {
        // Create a new label
        Label messageLabel = new Label();
        messageLabel.Text = text;

        // Set the label's color based on the condition
        messageLabel.Modulate = isPurchased ? new Godot.Color(0, 1, 0) : new Godot.Color(1, 0, 0); // Green for "Purchased", Red for "Not enough!"

        // Position the label at the button's position

        AddChild(messageLabel); // Add the label to the scene
        messageLabel.GlobalPosition = new Vector2(GlobalPosition.X + 55, GlobalPosition.Y + 60);

        // Wait for the animation duration before hiding and removing the label
        await ToSignal(GetTree().CreateTimer(messageDuration), "timeout");

        // Remove the label from the scene after the animation duration
        messageLabel.QueueFree();
    }

}
