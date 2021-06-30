using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.InputField))]
public class Field_IntegerExtender : MonoBehaviour
{
    private InputField field;

    public int value { get 
        {
            int _value = 0;

            bool isInt = int.TryParse(field.text, out _value);

            if (!isInt)
            {
                return minLimit;
            }
            else 
            {
                return _value;
            }
        }
    }

    [SerializeField] int minLimit;
    [SerializeField] int maxLimit;

    [Header("Other")]
    [SerializeField] Field_IntegerExtender parentField;
    [SerializeField] Field_IntegerExtender childField;
    [SerializeField] int valueDifference = -1;

    private void Awake()
    {
        field = GetComponent<InputField>();
        field.onEndEdit.AddListener((string value) => { AdjustField(); });
    }

    void AdjustField() 
    {
        int _value = 0;

        bool isInt = int.TryParse(field.text, out _value);

        if (!isInt) 
        {
            field.text = minLimit.ToString();
            return;
        }

        if (_value < minLimit)
        {
            field.text = minLimit.ToString();
        }

        if (_value > maxLimit)
        {
            field.text = maxLimit.ToString();
        }

        if (parentField != null && childField != null)
            return;

        if (parentField != null) 
        {
            if(value >= parentField.value)
                field.text = (parentField.value + valueDifference).ToString();
        }

        if (childField != null)
        {
            if(value < childField.value)
                childField.GetComponent<InputField>().text = (value + valueDifference).ToString();
        }
    }
}
