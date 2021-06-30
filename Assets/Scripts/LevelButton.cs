using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private GameObject lockedIcon;
    [SerializeField]
    private GameObject completedIcon;  
    [SerializeField]
    private AudioClip clickSound;

    private int levelIndex;
    private Button btn;
    private bool isLocked = false;

    private void OnEnable()
    {
        if(isLocked)
            GetComponent<Animator>().SetBool("Locked", isLocked);
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        GetComponent<Animator>().SetBool("Locked", isLocked);
    }

    internal void SetBtnData(int i)
    {
        levelIndex = i;
        levelText.text = (i + 1).ToString();

        if (PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED, 0) == i)
        {
            lockedIcon.SetActive(false);
            completedIcon.SetActive(false);
        }

        else if (PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED, 0) > i)
        {
            lockedIcon.SetActive(false);
            completedIcon.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(PlayerPrefKeys.LEVELS_UNLOCKED, 0) < i)
        {
            lockedIcon.SetActive(true);
            completedIcon.SetActive(false);
            isLocked = true;
            
        }
    }

    public void onClick_Button() 
    {
        if (!isLocked)
            LevelManager.instance.LoadLevel(levelIndex);
        else
            GameManager.instance.SetLockedDialog(true);

        SoundController.instance.PlayerSFX(clickSound);
    }
}
