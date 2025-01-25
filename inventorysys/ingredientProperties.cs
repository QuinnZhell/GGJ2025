using Godot;

[GlobalClass]
public partial class ingredientProperties : Resource
{
    [Export]
    public string Name { get; set; }

    //base stats which effect the potions overall score
    [Export]
    public float[3] BaseStats { get; set; }

    //Change over time stats which affect how the potion changes from tick to tick
    [Export]
    public float[3] DynamicStats { get; set; }

    //Effect on the potion when ingredient added initially
    [Export]
    public float[3] ImpulseStats { get; set; }


}