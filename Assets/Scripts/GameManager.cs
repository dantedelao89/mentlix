using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    private int totalLevels { get { return LevelManager.instance.levels.Length; } }

    [SerializeField]
    private GameObject levelBtn;
    [SerializeField]
    private GameObject noInteractionObj;
    [SerializeField]
    private Transform levelBtnParent;
    [SerializeField]
    private List<GameObject> levelBtns = new List<GameObject>();
    [SerializeField]
    private GameObject[] dialogs; //0 : lockedLevel | 1 : levelCompleted | 2 : SkipLevel | 3 : Insufficient | 4 : Clue
    [SerializeField]
    private GameObject[] clueDialogs;
    [SerializeField]
    private GameObject[] menuPanels;
    [SerializeField]
    private GameObject[] langButtons;
    [SerializeField]
    private GameObject black;


    // Start is called before the first frame update
    void Start()
    {
        SetupLevelBtns();
        SetupLangBtns();

        //aitcHUtils.MiscUtils.DoWithDelay(this, ()=> 
        //{
        //    black.SetActive(false);
        //}, 0.1f);
    }

    public void SetupLevelBtns() 
    {
        if (levelBtns.Count > 0) 
        {
            foreach (GameObject btn in levelBtns)
            {
                Destroy(btn);
            }
            levelBtns.Clear();
        }

        for (int i = 0; i < totalLevels; i++)
        {
            GameObject btn = Instantiate(levelBtn, levelBtnParent);
            btn.GetComponent<LevelButton>().SetBtnData(i);
            levelBtns.Add(btn);
        }
    }

    public void SetupLangBtns()
    {
        foreach (var btn in langButtons)
        {
            if (btn.name == PlayerPrefs.GetString("Language", "SP"))
            {
                btn.GetComponent<Text>().color = Color.black;
                btn.GetComponent<Text>().fontSize = 60;
            }
            else 
            {
                btn.GetComponent<Text>().color = Color.grey;
                btn.GetComponent<Text>().fontSize = 55;
            }
        }
    }



    public void onClick_StartGame() 
    {
        LevelManager.instance.LoadLevel(PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED) >= totalLevels ? 0 : PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED));
    }

    public void HideMenuPanels()
    {
        foreach (var panel in menuPanels)
        {
            panel.SetActive(false);
        }
    }

    public void SetInteraction(bool state) 
    {
        state = !state;
        noInteractionObj.SetActive(state);
    }

    public void SetLockedDialog(bool state)
    {
        dialogs[0].SetActive(state);
    }

    public void SetLevelCompletedDialog(bool state) 
    {
        dialogs[1].SetActive(state);
    }

    public void SetSkipLevelDialog(bool state)
    {
        dialogs[2].SetActive(state);
    }

    public void SetInsufficientFundDialog(bool state)
    {
        dialogs[3].SetActive(state);
    }

    public void SetClueDialog(bool state)
    {
        dialogs[4].SetActive(state);
    }

    public void GameCompletedDialog(bool state)
    {
        dialogs[5].SetActive(state);
    }

    public void ShowClueDialog(int levelIndex, bool state = true)
    {
        clueDialogs[levelIndex].SetActive(state);
    }

    public void ShowMessage() 
    {
    
    }

    public void onClick_SelectLanguage(GameObject langButton) 
    {
        foreach (var btn in langButtons)
        {
            if (btn == langButton) 
            {
                btn.GetComponent<Text>().color = Color.black;
                btn.GetComponent<Text>().fontSize = 60;
            }
            else
            {
                btn.GetComponent<Text>().color = Color.grey;
                btn.GetComponent<Text>().fontSize = 55;
            }
        }

        Localization.instance.ChangeLanguage(langButton.name);
    }
}

public class PlayerPrefKeys 
{
    public static string LEVELS_UNLOCKED = "1";
    public static string CURRENCY_AVAILABLE = "2";
}
