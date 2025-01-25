using Godot;
using System;

[GlobalClass]
public partial class Ingredient : Resource
{
    public Resource sprite;

    #region shop exposed variables
    [Export] public string name { get; set; }
    [Export] public string description { get; set; }
    [Export] public int baseCost { get; set; }
    #endregion

    #region Caldron exposed variables
    [Export] public IngredientProperties properties { get; set; }
    #endregion

    #region Cooldown usage controls
    [Export] public bool useCooldown;
    [Export] public bool useMultiplicative;
    [Export] public bool onCooldown;
    [Export] public float cdTime; //cooldown time after 1 use. 
    private float _cdTimer; //current running cooldown -1 if inactive
    #endregion 

    //name, description, cost, properties, use cooldown, use multiplicative, cooldown active, cd time for item, current cd timer.
    public Ingredient(string n, string d, int bc, IngredientProperties ip, bool uc, bool um, bool oc, float cd, float cdt)
    {
        name = n;
        description = d;
        baseCost = bc;
        properties = ip;
        useCooldown = uc;
        useMultiplicative = um;
        onCooldown = oc;
        cdTime = cd;
        _cdTimer = cdt;
    }

    //The above, but randomise the properties
    public Ingredient(string n, string d, int bc, bool uc, bool um, bool oc, float cd, float cdt)
    {
        name = n;
        description = d;
        baseCost = bc;
        properties = new IngredientProperties();
        useCooldown = uc;
        useMultiplicative = um;
        onCooldown = oc;
        cdTime = cd;
        _cdTimer = cdt;
    }



    public float GetCDTimer()
    {
        return _cdTimer;
    }

    public void SetCDTimer(float n)
    {
        _cdTimer = n;
    }
}
