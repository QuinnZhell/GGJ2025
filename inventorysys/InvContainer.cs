using Godot;
using System;

public partial class InvContainer : Node
{
    private string _name; //This is container name, not ingredient name. That is stored within Ingredient
    private int _quantity;
    private Ingredient _ingredient;
    private bool _empty;
    


    public InvContainer(string name, int quantity, Ingredient ingredient, bool empty)
    {
        _name = name;
        _quantity = quantity;
        _ingredient = ingredient;
        _empty = empty;
    }

    #region public methods

    //Gets number of the item remaining in inventory container
    public int GetQuantity()
    {
        return _quantity;
    }

    //Returns true if this item uses a cooldown
    public bool IsCooldownEnabled()
    {
        return _ingredient.useCooldown;
    }

    //Returns the time in seconds remaining until the item can be used. Returns -1 if cooldown inactive.
    public float GetCooldown()
    {
        return _ingredient.GetCDTimer();
    }

    //Returns the amount of time 1 unit of ingredient will cause it to be on cooldown for
    public float GetCooldownTime()
    {
        return _ingredient.cdTime;
    }

    //Returns true if cooldown is above 0 seconds
    public bool IsCoolingDown()
    {
        return _ingredient.onCooldown;
    }

    //Returns true if cooldown is multiplicative
    public bool isMultiplicative()
    {
        return _ingredient.useMultiplicative;
    }

    //Returns the ingredient stored by container
    public Ingredient GetIngredient()
    {
        return _ingredient;
    }

    //Returns true if container ingredient quantity is 0
    public bool IsEmpty()
    {
        return _empty;
    }

    //Increases container quantity by specified amount. 1 if not specified. Sets empty to false.
    public void Add(int number)
    {
        _quantity += number;
        _empty = false;
    }

    public void Add()
    {
        _quantity += 1;
        _empty = false;
    }

    //reduces container quantity by specified amount. 1 if not specified. Sets empty to true if quantity 0 or under. 
    public void Remove(int number)
    {
        _quantity -= number;
        if (_quantity <= 0)
        {
            _empty = true;
        }
    }

    public void Remove()
    {
        _quantity -= 1;
        if (_quantity <= 0)
        {
            _empty = true;
        }
    }

    //CALL IF USING THIS CONTAINER
    //Returns ingredient being used. Also handles cooldown setting and removes relevant quantity. Quantity 1 unless specified.
    public Ingredient Use(int n)
    {
        SetCooldown(n);
        Remove(n);
        return _ingredient;
    }

    public Ingredient Use()
    {
        SetCooldown();
        Remove();
        return _ingredient;
    }
    #endregion

    //Automated cooldown checker
    public override void _Process(double delta)
    {
        //Run cd timer only if required
        if (_ingredient.onCooldown)
        {
            TickCooldown(delta);
        }
    }

    #region cooldown functions
    //Sets cooldown to predetermined value, single multiple use unspecified. Only specified if cooldown is multiplicative
    private void SetCooldown(int n)
    {
        _ingredient.SetCDTimer(_ingredient.cdTime * n);
        _ingredient.onCooldown = true;
    }
    
    private void SetCooldown()
    {
        _ingredient.SetCDTimer(_ingredient.cdTime);
        _ingredient.onCooldown = true;
    }

    //Ticks cooldown and unsets flag if below 0. Also sets cd to -1 for return value. 
    private void TickCooldown(double t)
    {
        _ingredient.SetCDTimer((float)t);

        //Check negative values. Set to -1 for clear definition if true, and unset cooldown flag
        if (_ingredient.GetCDTimer() <= 0)
        {
            _ingredient.SetCDTimer(-1);
            _ingredient.onCooldown = false;
        }
    }
    #endregion
}
