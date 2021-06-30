using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField]
    private bool isCorrect = false;
    private GameObject resultObj;

    bool _animationPlaying = false;

    private void Awake()
    {
        //resultObj = transform.Find("Result").gameObject ;
        resultObj = transform.GetChild(transform.childCount - 1).gameObject;
    }

    private void OnEnable()
    {
        resultObj.SetActive(false);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(onClick_CheckIfCorrect);
    }

    public void onClick_CheckIfCorrect()
    {
        if (!FirebaseAnalyticController.instance.isLevelPlayLogged) 
        {
            FirebaseAnalyticController.instance.LogLevel("Played_Level_" + (LevelManager.instance.CurrentLevelIndex + 1).ToString());
            FirebaseAnalyticController.instance.isLevelPlayLogged = true;
        }
        if (!LevelManager.instance.IsCompleted)
        {
            if (!_animationPlaying)
            {
                if (isCorrect)
                    LevelManager.instance.LevelCompleted();

                resultObj.SetActive(true);
                GetComponentInChildren<Animator>().SetTrigger("show");

                _animationPlaying = true;

                aitcHUtils.MiscUtils.DoWithDelay(this, () =>
                {
                    _animationPlaying = false;
                }, 0.75f);
            }
        }
    }
}
