using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviourSingleton<CurrencyController>
{
    public int availableCurrency { get { return PlayerPrefs.GetInt(PlayerPrefKeys.CURRENCY_AVAILABLE, 2); } }

    [SerializeField]
    private Text[] brainsAvailableTxts;

    private void Start()
    {
#if UNITY_EDITOR
        //AddCurrency(1000);
#endif
        UpdateTexts();
    }

    /// <summary>
    /// Adds to total avaialbe currency
    /// </summary>
    /// <param name="amountToAdd">The amount to add in avaialable currency</param>
    public void AddCurrency(int amountToAdd) 
    {
        int currAvailable = availableCurrency;
        int newCurrAvailable = currAvailable + amountToAdd;

        PlayerPrefs.SetInt(PlayerPrefKeys.CURRENCY_AVAILABLE, newCurrAvailable);
        GameManager.instance.FillBrain();

        UpdateTexts();
    }

    /// <summary>
    /// Subtracts to total avaialbe currency. Amount can't be less than 0
    /// </summary>
    /// <param name="amountToSubtract">The amount to subtract in avaialable currency</param>
    public void SubtractCurrency(int amountToSubtract)
    {
        int currAvailable = availableCurrency;
        int newCurrAvailable = currAvailable - amountToSubtract;

        if (newCurrAvailable < 0)
            newCurrAvailable = 0;

        PlayerPrefs.SetInt(PlayerPrefKeys.CURRENCY_AVAILABLE, newCurrAvailable);

        UpdateTexts();
    }

    void UpdateTexts() 
    {
        foreach (var txt in brainsAvailableTxts)
        {
            txt.text = availableCurrency.ToString();
        }
    }
}
