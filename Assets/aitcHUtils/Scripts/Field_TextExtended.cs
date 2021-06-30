using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.InputField))]
public class Field_TextExtended : MonoBehaviour
{
    public enum TextType { UpperCase, LowerCase, Normal }
    [SerializeField] bool updateOnType = false;
    [SerializeField] bool alphabetOnly = false;
    [SerializeField] TextType textType;

    private InputField field;

    private void Start()
    {
        field = GetComponent<InputField>();
        if(updateOnType)
            field.onValueChanged.AddListener((string value) => { AdjustField(); });
        else
            field.onEndEdit.AddListener((string value) => { AdjustField(); });
    }

    void AdjustField()
    {
        int val = 0;
        if (textType == TextType.UpperCase)
        {
            field.text.ToUpper();
        }
        else if (textType == TextType.LowerCase)
        {
            field.text.ToLower();
        }

        if (alphabetOnly) 
        {
            if (field.text.Length > 0)
            {
                if (int.TryParse(field.text.ToCharArray()[field.text.Length - 1].ToString(), out val))
                {
                    field.text = field.text.Replace(field.text.ToCharArray()[field.text.Length - 1], "".ToCharArray()[0]);
                }
            }
        }


    }
}
