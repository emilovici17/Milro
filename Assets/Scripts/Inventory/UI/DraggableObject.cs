using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Vector3 startDragPosition;
    private Transform parent;

    #region Private

    private void Awake()
    {
        startPosition = transform.localPosition;
        parent = transform.parent;
    }

    private void Update()
    {

    }

    #endregion

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition.x = eventData.position.x - transform.position.x;
        startDragPosition.y = eventData.position.y - transform.position.y;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x - startDragPosition.x, eventData.position.y - startDragPosition.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        startDragPosition = transform.parent.position;
        transform.SetParent(parent);
        transform.localPosition = startPosition;
    }
}
