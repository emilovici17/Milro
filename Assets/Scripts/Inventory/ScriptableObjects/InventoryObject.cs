using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class used for managing inventory itens for a player entity
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    // TODO: Load it from Resources/ItemDatabase or from a sql db
    public ItemDatabaseObject Database;

    [SerializeField]
    private InventorySlot[] slots = new InventorySlot[15];
    public InventorySlot[] Slots { get => slots; }

    /// <summary>
    /// Adds an item and a quantity to an inventory slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    public void Add(ItemObject item, int quantity = 1)
    {
        // if the item is not stackable then add the item to an empty inventory slot
        if (!item.IsStackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].ItemID == -1)
                {
                    slots[i] = new InventorySlot(item, quantity);
                    return;
                }
            }
            return;
        }

        // If the item is stackable, 
        //      if found indentical item, then stack it
        //      else add it to a new slot
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].ItemID == item.ID)
            {
                slots[i].Quantity += quantity;
                return;
            }
        }
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].ItemID == -1)
            {
                slots[i] = new InventorySlot(item, quantity);
                return;
            }
        }
    }

    /// <summary>
    /// Removes an item from inventory
    /// </summary>
    /// <param name="item"></param>
    public void Remove(ItemObject item, int quantity = 1)
    {
        int remainedQuantity = quantity;
        // If the item exists then remove the item
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].ItemID == item.ID)
            {
                slots[i].Quantity -= quantity;

                if (slots[i].Quantity > 0)
                    return;
                else
                {
                    remainedQuantity = -quantity;
                    if (remainedQuantity == 0)
                        return;
                }
            }
        }
    }
}
