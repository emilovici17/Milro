using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "InventorySystem/Items/Equipment")]
public class EquipmentItemObject : ItemObject
{
    public void Awake()
    {
        itemType = ItemType.Equipment;
    }
}
