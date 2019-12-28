using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pistol Item", menuName = "InventorySystem/Items/Pistol")]
public class PistolItemObject : GunItemObject
{
    public void Awake()
    {
        itemType = ItemType.Gun;
        gunType = GunType.Pistol;
    }

}
