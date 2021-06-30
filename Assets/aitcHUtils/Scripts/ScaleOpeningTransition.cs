using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOpeningTransition : MonoBehaviour
{
    [SerializeField] float transitionDuration;

    private Coroutine lerpCoroutine;

    public void onClick_SetActive(bool open) 
    {
        if (lerpCoroutine != null) 
        {
            StopCoroutine(lerpCoroutine);
            lerpCoroutine = null;
        }
        lerpCoroutine = StartCoroutine(coroutine_Lerp(open));
        
    }

    IEnumerator coroutine_Lerp(bool open) 
    {
        if (open)
        {
            transform.localScale = Vector2.zero;
            while (transform.localScale.x < 1)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, transitionDuration);
                yield return null;
            }
        }
        else 
        {
            while (transform.localScale.x > 0)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, transitionDuration);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
