using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "InventorySystem/Items/Default")]
public class ItemObject : ScriptableObject
{
    #region PRIVATE

    [SerializeField]
    private int id;
    [SerializeField]
    protected ItemType itemType = ItemType.Default;
    [SerializeField]
    new private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    protected bool isStackable = true;

    #endregion

    #region Public

    public int ID { get { return id; } set { id = value; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public Sprite Icon { get { return icon; } }
    public ItemType ItemType { get { return itemType; } }
    public bool IsStackable { get { return isStackable; } }

    #endregion
}
