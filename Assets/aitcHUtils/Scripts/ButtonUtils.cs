using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUtils : MonoBehaviour
{
    [SerializeField] Button btn;
    public Image img;

    [SerializeField] float inactiveTime = 20f;
    [SerializeField] bool autoAssignFunction = true;

    private void Awake()
    {
        //btn = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(autoAssignFunction)
            btn.onClick.AddListener(btnClick);
    }

    public void btnClick() 
    {
        //StartCoroutine(coroutine_Timer());   
    }

    public void EnableBtn()
    {
        btn.interactable = true;
        img.fillAmount = 1f;
    }

    public void DisableBtn() 
    {
        btn.interactable = false;
        img.fillAmount = 0;
    }

    IEnumerator coroutine_Timer() 
    {
        btn.interactable = false;
        img.fillAmount = 0;
        float timer = 0;

        while (timer < inactiveTime)
        {
            timer += Time.deltaTime;
            img.fillAmount = timer / inactiveTime;
            yield return null; 
        }

        img.fillAmount = 1;
        btn.interactable = true;
    }
}
