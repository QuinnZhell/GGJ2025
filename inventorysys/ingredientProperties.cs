using Godot;
using System;

public enum Attributes {NONE, FIRE_RESISTANCE, WATER_BREATHING, FLIGHT, STRENGTH, SPEED, INVISIBILITY, COLD_RESISTANCE, RIZZ, MAGIC_BOOST, TELEPORTATION}




[GlobalClass]
public partial class IngredientProperties : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public string Description { get; set; }

    //base stats which effect the potions overall score
    [Export]
    public Vector3 BaseStats { get; set; }

    //Change over time stats which affect how the potion changes from tick to tick
    [Export]
    public Vector3 DynamicStats { get; set; }

    //Effect on the potion when ingredient added initially
    [Export]
    public Vector3 ImpulseStats { get; set; }

    //Attribute which ingredient adds
    [Export]
    public Attributes attribute { get; set; }

}