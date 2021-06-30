using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float transitionDuration;

    [SerializeField] bool noTransition = false;
    [SerializeField] bool hideOnComplete = false;

    private Coroutine lerpCoroutine;
    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void ShowPanel()
    {
        if (!noTransition)
        {
            rect.anchoredPosition = closePos;
            if (lerpCoroutine != null)
            {
                StopCoroutine(lerpCoroutine);
                lerpCoroutine = null;
            }
            lerpCoroutine = StartCoroutine(coroutine_LerpToDestination(true));

        }
    }

    //public void HidePanel()
    //{
    //    if (!noTransition)
    //    {
    //        rect.anchoredPosition = openPos;
    //        if (lerpCoroutine != null)
    //        {
    //            StopCoroutine(lerpCoroutine);
    //            lerpCoroutine = null;
    //        }
    //        lerpCoroutine = StartCoroutine(coroutine_LerpToDestination(false));
    //        MainGame.instance.GetComponent<TaskController>().ShowTaskList();
    //    }
    //    else
    //    {
            
    //            gameObject.SetActive(false);

    //        if (MainGame.instance.IsGameStarted)
    //            MainGame.instance.GetComponent<TaskController>().ShowTaskList();
    //    }
    //    ShortcutController.instance.EscapeKeyPressed = null;
    //    MainGame.instance.currentPlayer.canMove = true;
    //}

    IEnumerator coroutine_LerpToDestination(bool open)
    {
        if (open)
        {
            rect.anchoredPosition = closePos;
            while (Mathf.Abs((rect.anchoredPosition - openPos).sqrMagnitude) > 0.001f )
            {

                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, openPos, transitionDuration);
                yield return null;
            }
        }
        else
        {
            rect.anchoredPosition = openPos;
            while (Mathf.Abs((rect.anchoredPosition - closePos).sqrMagnitude) > 0.001f)
            {
                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, closePos, transitionDuration);
                yield return null;
            }

            if (hideOnComplete)
                gameObject.SetActive(false);
        }
    }
}
