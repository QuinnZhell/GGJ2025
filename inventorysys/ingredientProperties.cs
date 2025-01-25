using Godot;
using System;

public enum Attributes {NONE, FIRE_RESISTANCE, WATER_BREATHING, FLIGHT, STRENGTH, SPEED, INVISIBILITY, COLD_RESISTANCE, RIZZ, MAGIC_BOOST, TELEPORTATION}




[GlobalClass]
public partial class IngredientProperties : Resource
{
    #region vars
    //base stats which effect the potions overall score
    [Export]
    public Vector3 baseStats { get; set; }

    //Change over time stats which affect how the potion changes from tick to tick
    [Export]
    public Vector3 dynamicStats { get; set; }

    //Effect on the potion when ingredient added initially
    [Export]
    public Vector3 impulseStats { get; set; }

    //Attribute which ingredient adds
    [Export]
    public Attributes attribute { get; set; }
    #endregion

    public IngredientProperties (Vector3 bs, Vector3 ds, Vector3 ips, Attributes att)
    {
        baseStats = bs;
        dynamicStats = ds;
        impulseStats = ips;
        attribute = att;
    }

    //Randomise it
    public IngredientProperties()
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        //Randomise the bubble, potency, and stability scores
        Vector3 x = new Vector3(random.RandfRange(-1f, 1f), random.RandfRange(-1f, 1f), random.RandfRange(-1f, 1f));
        Vector3 y = new Vector3(random.RandfRange(-0.1f, 0.1f), random.RandfRange(-0.1f, 0.1f), random.RandfRange(-0.1f, 0.1f));
        Vector3 z = new Vector3(random.RandfRange(-0.5f, 0.5f), random.RandfRange(-0.5f, 0.5f), random.RandfRange(-0.5f, 0.5f));

        int a = random.RandiRange(0, 10);
        switch (a)
        {
            case 0:
                attribute = Attributes.NONE;
                break;

            case 1:
                attribute = Attributes.FIRE_RESISTANCE;
                break;

            case 2:
                attribute = Attributes.WATER_BREATHING;
                break;

            case 3:
                attribute = Attributes.FLIGHT;
                break;

            case 4:
                attribute = Attributes.STRENGTH;
                break;

            case 5:
                attribute = Attributes.SPEED;
                break;

            case 6:
                attribute = Attributes.INVISIBILITY;
                break;

            case 7:
                attribute = Attributes.COLD_RESISTANCE;
                break;

            case 8:
                attribute = Attributes.RIZZ;
                break;

            case 9:
                attribute = Attributes.MAGIC_BOOST;
                break;

            case 10:
                attribute = Attributes.TELEPORTATION;
                break;
        } //Assign random number to enum

        baseStats = x;
        dynamicStats = y;
        impulseStats = z;
    }
}