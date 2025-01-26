using Godot;
using System;

public enum Attributes {NONE, FIRE_RESISTANCE, WATER_BREATHING, FLIGHT, STRENGTH, SPEED, INVISIBILITY, COLD_RESISTANCE, RIZZ, MAGIC_BOOST, TELEPORTATION}




[GlobalClass]
public partial class IngredientProperties : Resource
{
    /*
     * Hello and welcome to my ingretient properties
     * here you will find ingredient properties
     * There are three Vector3s in this list
     * Theyre the properties represented as floats, and they should be read as follows
     * x = bubbles
     * y = potency
     * z = stability
     * 
     * base stats are the dormant, immutable stats of the ingredient. The final potion should average all of the base stats of all ingredients added to it
     * Dynamic stats are how much the bar should move per tick. Randomise and edit at will, but ticks are fast, and small number should be used. Potion bars should change by sum of dynamic stats of all ingredients
     * impulse stats are how much the bars change on the tick you add the ingredient to the potion. They have no lasting effect on the potion 
     * 
     * If you need to add more attributes, adjust 1. the enum at the top of this file, 2. the random number generator in the empty constructor class, 3. the switch case to allow the RNG to be used and your affect to be applied
     * Also, inventory generates based on a hard coded number. This number is found in the loop and then subseqyent switch in inventory CS, Generate() method. YOu need to change that if you change this too. 
     * 
     */


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
   

    #region constructors
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

        RNGStats(random);

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

    }

    //Randomise all, but set a known attribute
    public IngredientProperties (Attributes x)
    {
        var random = new RandomNumberGenerator();
        random.Randomize();

        attribute = x;
        
        RNGStats(random);
    }

    #endregion

    #region accessors

    public Vector3 GetBaseStats()
    {
        return baseStats;
    }

    public Vector3 GetDynamicStats()
    {
        return dynamicStats;
    }

    public Vector3 GetImpulseStats()
    {
        return impulseStats;
    }

    public Attributes GetAttribute()
    {
        return attribute;
    }

    #endregion

    #region private methods

    private void RNGStats(RandomNumberGenerator random)
    {
        
        //Randomise the bubble, potency, and stability scores
        Vector3 x = new Vector3(random.RandfRange(-1f, 1f), random.RandfRange(-1f, 1f), random.RandfRange(-1f, 1f));
        Vector3 y = new Vector3(random.RandfRange(-0.1f, 0.1f), random.RandfRange(-0.1f, 0.1f), random.RandfRange(-0.1f, 0.1f));
        Vector3 z = new Vector3(random.RandfRange(-0.5f, 0.5f), random.RandfRange(-0.5f, 0.5f), random.RandfRange(-0.5f, 0.5f));


        baseStats = x;
        dynamicStats = y;
        impulseStats = z;
    }

    #endregion
}