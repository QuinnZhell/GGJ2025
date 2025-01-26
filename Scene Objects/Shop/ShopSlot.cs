using Godot;
using System;
using System.Drawing;

public partial class ShopSlot : Button
{

    //Get reference to players inv here
    //public Inventory inv;

    //Temp int for cash till we get inv, change to reference to players current cash
	int IngredientPrice;
    int FinalPrice;
    //[Export] private Ingredient _ingredient;
    private PopupPanel _popUp;
    private AudioStreamPlayer2D sfx;
    GoldDisplay display;

    // Duration for which the message will be shown
    private const float messageDuration = 1.0f;

    public override void _Ready()
    {
        _popUp = GetNode<PopupPanel>("ItemInfo");
        Control _control = _popUp.GetNode<Control>("Control");
        RichTextLabel _itemName = _control.GetNode<RichTextLabel>("Item Name");
        RichTextLabel _price = _control.GetNode<RichTextLabel>("Price");
        RichTextLabel _desc = _control.GetNode<RichTextLabel>("Desc");
        display = GetParent().GetNode<GoldDisplay>("Gold");
        sfx = GetNode<AudioStreamPlayer2D>("ShopSFX");

        UpdatePopupPosition();

        int SalePercentage = 0;
        int x = Convert.ToInt32(GD.Randi() % 49);

        if (x >= 1 && x <= 3)
        {
            SalePercentage = x * 10;
        }

        // FinalPrice = _ingredient.baseCost * (100 - SalePercentage) / 100;
        FinalPrice = 20;

        //_itemName.Text = _ingredient.name;
       // _desc.Text = _ingredient.description;

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
        GameMaster.inventory.setGold(TryToBuy(GameMaster.inventory.GetCoins()));
        display.UpdateGold();
        
    }

    private int TryToBuy(int cash)
    {
        if (cash >= FinalPrice)
        {
            cash -= FinalPrice;
            ShowMessage("Purchased", true);
            sfx.Stream = GD.Load<AudioStream>("res://GGJ2025ArtAssets/SFX/Purchase.wav");
            sfx.Play();
            //Add 1 of item to inventory here..
        }
        else
        {
            ShowMessage("Not Enough!", false);
            sfx.Stream = GD.Load<AudioStream>("res://GGJ2025ArtAssets/SFX/laugh sound effect.wav");
            sfx.Play();
        }

        return cash;
    }

    private void UpdatePopupPosition()
    {
        if (_popUp != null)
        {
            // Make the popup position relative to the button.
            _popUp.Position = new Vector2I((int)Position.X + 150, (int)Position.Y + 50);  // Convert to Vector2I
        }
    }
    private async void ShowMessage(string text, bool isPurchased)
    {
        Label messageLabel = new Label();
        messageLabel.Text = text;
        messageLabel.AddThemeFontSizeOverride("font_size", 24); // Increase font size

        messageLabel.Modulate = isPurchased ? new Godot.Color(0, 1, 0) : new Godot.Color(1, 0, 0);

        AddChild(messageLabel);
        messageLabel.GlobalPosition = new Vector2(GlobalPosition.X + 55, GlobalPosition.Y + 60);

        await ToSignal(GetTree().CreateTimer(messageDuration), "timeout");

        messageLabel.QueueFree();
    }

}
