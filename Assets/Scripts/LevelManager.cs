using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    private bool isCompleted = false;
    public bool IsCompleted { get { return isCompleted; } }

    public GameObject[] levels;

    [SerializeField]
    private GameObject levelPanel;

    private int currentLevelIndex;

    public int CurrentLevelIndex { get { return currentLevelIndex; } }


    public void LevelCompleted()
    {
        Debug.Log("Complet");
        isCompleted = true;
        GameManager.instance.SetInteraction(false);

        if (currentLevelIndex >= PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED)) // If level is completed for first time
        {
            FindObjectOfType<FBAppEventController>().LogAchievedLevelEvent("level_" + (currentLevelIndex + 1).ToString());
            int nextLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED) + 1;
            PlayerPrefs.SetInt(PlayerPrefKeys.LEVELS_UNLOCKED, nextLevelIndex);
            GameManager.instance.SetupLevelBtns();
        }

        aitcHUtils.MiscUtils.DoWithDelay(this, () =>
        {
            GameManager.instance.SetLevelCompletedDialog(true);
            GameManager.instance.SetInteraction(true);
        }, 1f);
    }

    public void onClick_NextLevel()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED) > currentLevelIndex)
        {
            LoadLevel(currentLevelIndex + 1);
            GameManager.instance.SetLevelCompletedDialog(false);
        }
        else
        {
            GameManager.instance.SetSkipLevelDialog(true);
        }
    }

    public void LoadLevel(int levelIndex, bool isReset = true)
    {
        FirebaseAnalyticController.instance.isLevelPlayLogged = false;

        GameManager.instance.HideMenuPanels();
        levelPanel.SetActive(true);

        if (levelIndex > levels.Length - 1)
        {
            Debug.LogError("Stating 1");
            levelIndex = 0;
            GameManager.instance.GameCompletedDialog(true);
        }

        for (int i = 0; i < levels.Length; i++)
        {
            if (i == levelIndex)
                levels[i].SetActive(true);
            else
                levels[i].SetActive(false);
        }

        isCompleted = false;
        currentLevelIndex = levelIndex;

        Debug.Log("Current Level is " + (currentLevelIndex + 1).ToString());



        if (isReset)
            ResetLevel();
    }

    public void onClick_ShowClue() 
    {
        if (CurrencyController.instance.availableCurrency >= 1)
        {
            CurrencyController.instance.SubtractCurrency(1);
            GameManager.instance.ShowClueDialog(currentLevelIndex);
        }
        else
        {
            GameManager.instance.SetInsufficientFundDialog(true);
        }
        GameManager.instance.SetClueDialog(false);
    }

    public void onClick_SkipLevel()
    {
        if (CurrencyController.instance.availableCurrency >= 2)
        {
            CurrencyController.instance.SubtractCurrency(2);
            LevelCompleted();
        }
        else
        {
            GameManager.instance.SetInsufficientFundDialog(true);
        }
        GameManager.instance.SetSkipLevelDialog(false);
    }

    void ResetLevel()
    {

    }
}
