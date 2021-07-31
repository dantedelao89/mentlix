using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class AdmobManager : MonoBehaviourSingleton<AdmobManager>
{
    private RewardedAd rewardedAd;
    
    [SerializeField]
    private string rewardedId = "ca-app-pub-3899069197542431/4849930756";
    [SerializeField]
    private bool ShowTestAds;

    private int loadRetries = 0;
    private int maxLoadRetries = 3;

    private bool isLoading = false;
    private Coroutine adAvailabilityCoroutine;

    private void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
        .build();
        requestConfiguration.ToBuilder().SetMaxAdContentRating(MaxAdContentRating.G);

        MobileAds.SetRequestConfiguration(requestConfiguration);

        if (ShowTestAds)
            rewardedId = "ca-app-pub-3940256099942544/5224354917";

        LoadRewardedAd();
    }

    private void LoadRewardedAd() 
    {


        isLoading = true;
        this.rewardedAd = new RewardedAd(rewardedId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isLoading = false;
        Debug.LogError(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.LoadAdError);

        if (loadRetries < maxLoadRetries)
        {
            Debug.Log("Retrying to load...");
            LoadRewardedAd();
            loadRetries++;
        }
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        isLoading = false;
        Debug.LogError(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        isLoading = false;
        Debug.LogWarning("HandleRewardedAdClosed event received");
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        isLoading = false;
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        loadRetries = 0;
        RewardBrains();
        LoadRewardedAd();
    }

    public void ShowRewardedVideo()
    {
        Debug.Log("id is " + rewardedId);

        if (this.rewardedAd.IsLoaded())
        {
            Debug.Log("id2 is " + rewardedId);
            this.rewardedAd.Show();
            return;
        }
        else if (!isLoading)
        {
            Debug.Log("id3 is " + rewardedId);
            LoadRewardedAd();

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                RewardBrains();
            }
        }
        else 
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                RewardBrains();
            }
        }


    }

    IEnumerator coroutine_CheckIfAdsWorking() 
    {
        yield return new WaitForSeconds(2f);

        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
        else
            RewardBrains();
    }

    void RewardBrains() 
    {
        CurrencyController.instance.AddCurrency(1);
        GameManager.instance.SetInsufficientFundDialog(false);

        if (LevelManager.instance.IsCompleted) 
        {
            LevelManager.instance.onClick_NextLevel();
        }
    }
}