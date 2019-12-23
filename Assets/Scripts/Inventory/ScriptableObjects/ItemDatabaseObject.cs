using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDatabase", menuName = "InventorySystem/Items/ItemDatabase")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private ItemObject[] items;

    private Dictionary<int, ItemObject> itemsDict = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].ID = i;
            itemsDict.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        itemsDict = new Dictionary<int, ItemObject>();
    }

    public ItemObject GetItem(int _id)
    {
        ItemObject result;

        itemsDict.TryGetValue(_id, out result);

        return result;
    }
}
