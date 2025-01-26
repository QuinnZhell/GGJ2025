using Godot;
using System;
using System.Linq;

public partial class Inventory : Node
{
	private Godot.Collections.Array<InvContainer> _containerArray;
	private int _coins;


    #region methods

    #region Add
    //Adds an inventory slot. This can either be pre made invcontainer object, or param set.
    //Prepopulated
    //MUST MUST MUST. Be a fully populated container as this array will be used by every system. Specify randomise if unfinished ingredient params. 
    public void Add(InvContainer container)
    {
        _containerArray.Append(container);
    }
    
    //Inv Container unpopulated, ingredient provided
    //Container name, quantity of ingredient, ingredient, empty, 
    public void Add(string cn, int q, Ingredient i, bool e)
    {
        InvContainer temp = new InvContainer(cn, q, i, e);
        _containerArray.Append(temp);
    }

    //Container & Ingredient unpopulated, properties provided
    public void Add(string cn, int q, string n, string d, int bc, IngredientProperties ip, bool uc, bool um, bool oc, float cd, float cdt, bool e)
    {
        Ingredient tempi = new Ingredient(n, d, bc, ip, uc, um,  oc, cd, cdt);
        InvContainer temp = new InvContainer(cn, q, tempi, e);
        _containerArray.Append(temp);
    }
    //All unpopulated
    public void Add(string cn, int q, string n, string d, int bc, Vector3 bs, Vector3 ds, Vector3 ips, Attributes att, bool uc, bool um, bool oc, float cd, float cdt, bool e)
    {
        IngredientProperties tempip = new IngredientProperties(bs, ds, ips, att);
        Ingredient tempi = new Ingredient(n, d, bc, tempip, uc, um, oc, cd, cdt);
        InvContainer temp = new InvContainer(cn, q, tempi, e);
        _containerArray.Append(temp);
    }
    //Everything else, attribute included, but randomise stats
    public void Add(string cn, int q, string n, string d, int bc, Attributes att, bool uc, bool um, bool oc, float cd, float cdt, bool e)
    {
        IngredientProperties tempip = new IngredientProperties(att);
        Ingredient tempi = new Ingredient(n, d, bc, tempip, uc, um, oc, cd, cdt);
        InvContainer temp = new InvContainer(cn, q, tempi, e);
        _containerArray.Append(temp);
    }


    #endregion

    #region remove
    //Remove selected int. If none specified, remove final item
    public void Remove()
    {
        _containerArray.Resize(_containerArray.Count());
    }

    public void Remove(int i)
    {
        _containerArray.RemoveAt(i);
    }
    #endregion

    #region GetContainer
    //Specify index and get an inv container.
    public InvContainer GetContainer(int i)
    {
        return _containerArray[i];
    }
    #endregion

    #region Getters

    public Godot.Collections.Array<InvContainer> GetInvevntory()
    {
        return _containerArray;
    }

    public int GetCoins()
    {
        return _coins;
    }

    #endregion

    #region Gold Management

    //Add gold. 1 if not specified
    public void AddGold()
    {
        _coins += 1;

    }
    public void AddGold(int i)
    {
        _coins += i;
    }

    //remove gold. 1 if not specified.

    public void RemoveGold()
    {
        _coins -= 1;
    }
    public void RemoveGold(int i)
    {
        _coins -= i;
    }

    #endregion

    #region initial inventory generation

    //bool defines wether to clear existing inventory pool
    public void Generate(bool clear)
    {

        if (clear)
        {
            _containerArray.Resize(0);
        }
        //Generates a set of items with usable attributes
        //Hardcoded for now as descriptions and item types known
        //proc gen is a stretch goal

        Add(
            "Tesseract of Mystic Boxes",
            0,
            "Mystic Box",
            "I can't seem to stop misplacing this thing...",
            69,
            Attributes.TELEPORTATION,
            false, 
            false, 
            false,
            1.5f,
            -1f,
            true);


        Add(
            "Bunch of Saxifraga",
            0,
            "Saxifrage",
            "The most charismatic of flower",
            69,
            Attributes.RIZZ,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Pouch of Manticore Feathers",
            0,
            "Manticore Feather",
            "Sometimes a few eggs must be broken to make an omelette. R.I.P. Glimbo.",
            69,
            Attributes.FLIGHT,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Jar of Newt Eyes",
            0,
            "Eye of Newt",
            "He's probably not getting better.",
            69,
            Attributes.INVISIBILITY,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Jar of Ochre Jelly",
            0,
            "Ochre Jelly",
            "It wobbles, if you look at it right",
            69,
            Attributes.MAGIC_BOOST,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "6 Pack of Mysterious Liquid",
            0,
            "Mysterious Liquid",
            "Brewed from iron with no extra additives",
            69,
            Attributes.FIRE_RESISTANCE,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Box of Waterleaves",
            0,
            "Waterleaf",
            "This was probably a frogs home, once. I wonder what kind of of potion a frog would make",
            69,
            Attributes.WATER_BREATHING,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Dish of Rock Moss",
            0,
            "Rock Moss",
            "Dwayne the Moss Johnson",
            69,
            Attributes.STRENGTH,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

        Add(
            "Bundle of Sparkthistles",
            0,
            "Sparkthistle",
            "Legend says if you put them to your ear, it will hurt",
            69,
            Attributes.SPEED,
            false,
            false,
            false,
            1.5f,
            -1f,
            true);

    }

    private Attributes InterpretIntToAttribute(int i)
    {
        switch (i)
        {
            case 0:
                return Attributes.NONE;

            case 1:
                return Attributes.FIRE_RESISTANCE;

            case 2:
                return Attributes.WATER_BREATHING;

            case 3:
                return Attributes.FLIGHT;

            case 4:
                return Attributes.STRENGTH;

            case 5:
                return Attributes.SPEED;

            case 6:
                return Attributes.INVISIBILITY;

            case 7:
                return Attributes.COLD_RESISTANCE;

            case 8:
                return Attributes.RIZZ;

            case 9:
                return Attributes.MAGIC_BOOST;

            case 10:
                return Attributes.TELEPORTATION;

            default:
                return Attributes.NONE;

        }

    }

    #endregion

    #endregion
}
