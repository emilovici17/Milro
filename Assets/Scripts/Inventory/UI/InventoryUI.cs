using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region CONSTANTS

    public const int X_START = -360;
    public const int Y_START = 300;
    public const int X_SPACE_BETWEEN = 110 + 10;
    public const int Y_SPACE_BEWTEEN = 110 + 10;
    public const int NR_OF_COLUMNS = 3;

    #endregion

    #region PRIVATE VARIABLES

    [SerializeField]
    private InventoryObject inventory;

    [SerializeField]
    private GameObject inventorySlotPrefab;

    private GameObject[] itemsDisplayed;

    private MouseHandler mouseHandler = new MouseHandler();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
        UpdateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create the inventory slots
    private void CreateSlots()
    {
        itemsDisplayed = new GameObject[inventory.Slots.Length];

        // Display every item from the inventory
        for(int i = 0; i < inventory.Slots.Length; i++)
        {
            var slotUI = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            RectTransform rect = slotUI.GetComponent<RectTransform>() as RectTransform;
            rect.localPosition = GetSlotPosition(i);

            itemsDisplayed[i] = slotUI;

            InventorySlot slot = inventory.Slots[i];
            InventorySlotUI s = itemsDisplayed[i].GetComponent<InventorySlotUI>();
            s.slot = slot;

            // Register event methods
            s.StartDrag += mouseHandler.OnStartDrag;
            s.EndDrag += mouseHandler.OnEndDrag;
            mouseHandler.SwitchSlots += SwitchSlots;
        }
    }

    // Update the inventory slots (items)
    private void UpdateSlots()
    {
        for (int i = 0; i < inventory.Slots.Length; i++)
        {
            InventorySlot slot = inventory.Slots[i];
            
            ItemObject itemObject = inventory.Database.GetItem(slot.ItemID);
            var slotUI = itemsDisplayed[slot.Index];

            var slotUIChild = slotUI.transform.GetChild(0);

            if(itemObject != null)
            {
                slotUIChild.gameObject.SetActive(true);
                slotUIChild.GetComponent<Image>().sprite = inventory.Database.GetItem(slot.ItemID).Icon;
                slotUIChild.GetComponent<Image>().raycastTarget = false;
                slotUIChild.GetComponentInChildren<TextMeshProUGUI>().text = slot.Quantity.ToString();            
            }
            else
            {
                slotUIChild.gameObject.SetActive(false);
            }


        }
    }

    private void SwitchSlots(InventorySlot firstSlot, InventorySlot secondSlot)
    {
        inventory.Switch(firstSlot, secondSlot);
        UpdateSlots();
    }

    private Vector3 GetSlotPosition(int index)
    {
        Vector3 result = new Vector3(X_START + (X_SPACE_BETWEEN * (index % NR_OF_COLUMNS)),
                                    Y_START + (-Y_SPACE_BEWTEEN * (index / NR_OF_COLUMNS)), 0);


        return result;
    }
    public class MouseHandler
    {
        private InventorySlot firstSlot = null;
        private InventorySlot secondSlot = null;

        public delegate void SwitchSlotHandler(InventorySlot firstSlot, InventorySlot secondSlot);
        public event SwitchSlotHandler SwitchSlots;

        public void OnStartDrag(InventorySlot slot)
        {
            firstSlot = slot;
        }

        public void OnEndDrag(InventorySlot slot)
        {
            secondSlot = slot;

            if(firstSlot != null && secondSlot != null)
            {
                Debug.Log("yes");

                if(firstSlot.Index != secondSlot.Index)
                    SwitchSlots(firstSlot, secondSlot);
            }
            else
            {
                Debug.Log("no");
            }

            firstSlot = null;
            secondSlot = null;
        }

    }
}
