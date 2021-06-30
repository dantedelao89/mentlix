using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LanguageAsset")]
public class LanguageAsset : ScriptableObject
{
    public LanguageInfo[] Languages;
    //public Dictionary<LanguageInfo> Languages = new System.Collections.Generic.Dictionary<LanguageInfo>();

    public LanguageInfo GetLanguageData(string langCode) 
    {
        foreach (var lang in Languages)
        {
            if (lang.languageCode == langCode)
                return lang;
        }

        return Languages[0];
    }

    [System.Serializable]
    public class LanguageInfo 
    {
        public string LanguageName;
        public bool AllowLanguage;
        public string languageCode;
        public Font languageFont;
    }
}


