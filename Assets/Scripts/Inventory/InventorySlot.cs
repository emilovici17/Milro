using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing an inventory slot
/// Used to store the item and its quantity
/// </summary>
[System.Serializable]
public class InventorySlot
{
    [SerializeField]
    private int itemID;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private int index;

    #region Constructors

    public InventorySlot()
    { itemID = -1; quantity = 0; }

    public InventorySlot(ItemObject _itemObject, int _quantity = 1)
    {
        itemID = _itemObject.ID;
        quantity = _quantity;
    }

    #endregion

    #region Public

    public int ItemID { get { return itemID; } }
    public int Quantity 
    { 
        get { return quantity; }
        set 
        { 
            if(value < 0)
            {
                return;
            }
            quantity = value;
        }
    }

    public int Index { get => index; set { index = value; } }

    #endregion
}
