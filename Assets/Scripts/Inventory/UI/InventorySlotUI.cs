using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image backgroundImage;
    private Color backgroundColor;

    private Vector3 startPosition;
    private Vector3 startDragPosition;
    private Transform parent;
    private Transform child;

    public delegate void SlotHandler(InventorySlot slot);
    public event SlotHandler StartDrag;
    public event SlotHandler EndDrag;

    public InventorySlot slot;

    public void Awake()
    {
        backgroundImage = GetComponent<Image>() as Image;
        backgroundColor = backgroundImage.color;

        parent = transform;
        child = transform.GetChild(0);
        startPosition = child.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.color = backgroundColor;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition.x = eventData.position.x - child.position.x;
        startDragPosition.y = eventData.position.y - child.position.y;
        child.SetParent(parent.parent);

        Image icon = child.gameObject.GetComponent<Image>() as Image;

        StartDrag(slot);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        child.position = new Vector3(eventData.position.x - startDragPosition.x, eventData.position.y - startDragPosition.y, 0);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        startDragPosition = parent.position;
        child.SetParent(parent);
        child.localPosition = startPosition;

        Image icon = child.gameObject.GetComponent<Image>() as Image;

        InventorySlotUI slotUI = (eventData.pointerCurrentRaycast.gameObject != null) 
            ? eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlotUI>() : null;
        
        EndDrag((slotUI != null) ? slotUI.slot : null);
    }
}
