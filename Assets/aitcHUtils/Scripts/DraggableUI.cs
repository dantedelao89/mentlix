using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] UIType uiType;
    [SerializeField] [Tooltip("The threshold after which item is not interactable")] float dragThreshold;

    private Vector2 beginDragPos;

    RectTransform rectTransform;
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDragPos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (uiType == UIType.Button) 
        {
            if ((rectTransform.anchoredPosition - beginDragPos).sqrMagnitude > dragThreshold * dragThreshold) 
            {
                Button tempBtn = GetComponent<Button>();

                if(tempBtn != null) tempBtn.interactable = false;
                else Debug.LogError("Draggable UI is of type Button with no Button Component Attached!");
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (uiType == UIType.Button)
        {
            Button tempBtn = GetComponent<Button>();

            if (tempBtn != null) tempBtn.interactable = true;
            else Debug.LogError("Draggable UI is of type Button with no Button Component Attached!");
        }
    }
}
public enum UIType { Image, Button }
