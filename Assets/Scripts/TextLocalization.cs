using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLocalization : MonoBehaviour
{
    //[Tooltip("0 for EN. 1 for FR. 2 for UR. 3 for HN. 4 for SP. 5 for IT")]
    //public string[] TextValues;
    [SerializeField] string TextKey;
    [SerializeField] Text txt;

    private string currentText = "???";
    private Font currentFont;

    // Start is called before the first frame update
    void Awake()
    {
        currentText = GetComponent<Text>().text;
    }

    private void OnEnable()
    {
        txt = GetComponent<Text>();
        UpdateDisplay();
    }

    public void UpdateText(Font languageFont) 
    {
        //Debug.Log(languageFont);

        try
        {
            //txt.text = Localization.LanguageJSON[PlayerPrefs.GetString("Language", "SP")][TextKey];
            currentText = Localization.LanguageJSON[PlayerPrefs.GetString("Language", "SP")][TextKey];
        }
        catch 
        {
            //txt.text = Localization.LanguageJSON["SP"][TextKey];
            currentText = Localization.LanguageJSON["SP"][TextKey];
        }
        currentFont = languageFont;

        if (txt != null)
            UpdateDisplay();
    }

    void UpdateDisplay() 
    {
        txt.text = currentText;
        txt.font = currentFont;
    }

}
