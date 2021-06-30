using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Localization : MonoBehaviour
{
    public static Localization instance;
    public static JSONNode LanguageJSON { get { return JSONNode.Parse(jsonString); } }

    static string jsonString = "";
    public LanguageAsset langAsset;
    TextLocalization[] localizedItems;

    [SerializeField] string webURL;


    void Awake()
    {
        instance = this;
        localizedItems = Resources.FindObjectsOfTypeAll(typeof( TextLocalization)) as TextLocalization[];
        if (jsonString == "") 
        {
            StartCoroutine(LoadLocalizedText((status) =>
            {
                if (!status)
                {
                    //Failed
                    Debug.LogError("Failed to load languages!! Try restarting game");
                    return;
                }
                else 
                {
                    UpdateLanguage(PlayerPrefs.GetString("Language", "SP"));
                }
        
            }));
        }

        //aitcHUtils.MiscUtils.DoWithDelay(this, () =>
        //{
        //}, 0.1f);

        //Debug.Log(jsonString);
    }

    public void ChangeLanguage(string langCode) 
    {
        UpdateLanguage(langCode);
    }

    void UpdateLanguage(string langCode) 
    {
        foreach (var item in localizedItems)
        {
            PlayerPrefs.SetString("Language", langCode);
            item.UpdateText(langAsset.GetLanguageData(langCode).languageFont);
        }
    }

    IEnumerator GetLanguageData() 
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error retriving language data!! Please check you connection and restart the game.");
            jsonString = "";
        }
        else 
        {
            jsonString = www.downloadHandler.text;
            Debug.Log("Success");
            UpdateLanguage(PlayerPrefs.GetString("Language", "SP"));
        }
    }

    IEnumerator LoadLocalizedText(Action<bool> success)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Language.json");

        string dataAsJson = "";

        //Android
        if (filePath.Contains("://"))
        {

            WWW reader = new WWW(filePath);

            //Wait(Non blocking until download is done)
            while (!reader.isDone)
            {
                yield return null;
            }

            if (reader.text == null || reader.text == "")
            {
                success(false);

                //Just like return false
                yield break;
            }

            dataAsJson = reader.text;
        }

        //iOS
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }


        jsonString = dataAsJson;

        if (dataAsJson == "") 
        {
            success(false);
            UpdateLanguage(PlayerPrefs.GetString("Language", "SP"));
        }

        else
        {
            success(true);
        }
    }
}
