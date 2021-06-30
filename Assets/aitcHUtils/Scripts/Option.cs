using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Option : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] bool interactable;
    [Tooltip("Drop all the options connected with this object")]
    [SerializeField] Option[] AllOptions;
    [SerializeField] Image graphic;
    [SerializeField] Selectable.Transition transition;

    [Header("Color Tint")]
    [SerializeField] Image targetGraphic ;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color highlightedColor = Color.white;
    [SerializeField] Color pressedColor = Color.white;
    [SerializeField] Color selectedColor = Color.white;
    [SerializeField] Color disabledColor = Color.grey;

    public bool isOn;
    public int selectedOptionIndex 
    { 
        get 
        {
            int index = 0;
            for (int i = 0; i < AllOptions.Length; i++)
            {
                if (AllOptions[i].isOn) 
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }

    [SerializeField] ValueChangeEvent OnSelect;

    private bool oldVal;

    private void OnEnable()
    {
        int x = 0;
        for (int i = 0; i < AllOptions.Length; i++)
        {
            if (AllOptions[i].isOn)
                x++;
        }

        if (x > 1 || x <= 0) 
        {
            for (int i = 0; i < AllOptions.Length; i++)
            {
                if (i == 0) 
                {
                    AllOptions[i].isOn = true;
                    continue;
                }

                AllOptions[i].isOn = false;
            }
        }
    }

    private void Start()
    {
        oldVal = isOn;    
    }

    
    private void Update()
    {
        if (isOn != oldVal) 
        {
            if (isOn) 
            {
                UpdateAllOptions();
            }
            oldVal = isOn;
        }

        if (isOn)
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 1);
        else
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
    }

    public void Select()
    {
        isOn = true;
        UpdateAllOptions();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOn = true;
        for (int i = 0; i < AllOptions.Length; i++)
        {
            if (AllOptions[i] == this)
                continue;

            AllOptions[i].isOn = false;
        }

        OnSelect.Invoke();
    }

    private void UpdateAllOptions() 
    {
        for (int i = 0; i < AllOptions.Length; i++)
        {
            if (AllOptions[i] == this)
                continue;

            AllOptions[i].isOn = false;
        }
    }
}
[System.Serializable]
public class ValueChangeEvent : UnityEvent
{
}