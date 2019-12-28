using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pistol Item", menuName = "InventorySystem/Items/Rifle")]
public class RifleItemObject : GunItemObject
{
    public void Awake()
    {
        itemType = ItemType.Gun;
        gunType = GunType.Rifle;
    }
}