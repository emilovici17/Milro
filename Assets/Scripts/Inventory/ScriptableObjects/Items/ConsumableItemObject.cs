using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "InventorySystem/Items/Consumable")]
public class ConsumableItemObject : ItemObject
{
    // TO DO: implement and add effects attributes (i.e. health, energy)
    public void Awake()
    {
        itemType = ItemType.Consumable;   
    }
}
