using Godot;
using System;
using System.Linq;

public partial class Inventory : Node
{
	private Godot.Collections.Array<InvContainer> _containerArray;
	private int _coins = GameMaster.STARTING_GOLD;

    public void setGold(int gold)
    {
        _coins = gold;
    }
    public int getGold()
    {
        return _coins;
    }

    #region Accessor methods

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

    #endregion
}
