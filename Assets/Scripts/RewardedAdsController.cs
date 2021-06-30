using UnityEngine;
using AudienceNetwork;

public class RewardedAdsController : MonoBehaviour
{
    [SerializeField]
    private bool ShowTestAds;
    [SerializeField]
    private string rewardedId;

    private RewardedVideoAd rewardedVideoAd;
    private bool isLoading = false;
    private bool isLoaded;
    private bool closedWithSuccess;
    // UI elements in scene
    //public Text statusLabel;

    int loadTries = 0;

    private void Awake()
    {
        isLoading = false;
        AudienceNetworkAds.Initialize();
        SettingsScene.InitializeSettings();
    }

    private void Start()
    {
        Debug.Log("Device id: " + SystemInfo.deviceUniqueIdentifier) ;

        if (ShowTestAds)
            rewardedId = "VID_HD_16_9_15S_APP_INSTALL#" + rewardedId;

        if (AudienceNetworkAds.IsInitialized())
            LoadRewardedVideo();
        else
            AudienceNetworkAds.Initialize();
    }

    //private void Update()
    //{
    //    Debug.Log("Rewarded is ");
    //}

    public void LoadRewardedVideo()
    {
        if (isLoading) // Loading already in progress
            return;

        isLoading = true;
        closedWithSuccess = false;

        // Create the rewarded video unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        this.rewardedVideoAd = new RewardedVideoAd(rewardedId);

        this.rewardedVideoAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.rewardedVideoAd.RewardedVideoAdDidLoad = (delegate ()
        {
            Debug.Log("RewardedVideo ad loaded.");
            this.isLoading = false;
            this.isLoaded = true;
        });
        this.rewardedVideoAd.RewardedVideoAdDidFailWithError = (delegate (string error)
        {
            Debug.Log("RewardedVideo ad failed to load with error: " + error);
            this.isLoading = false;
            aitcHUtils.MiscUtils.DoWithDelay(this, () =>
            {
                this.loadTries++;
                if (loadTries > 5)
                    loadTries = 0;
                else 
                {
                    Debug.Log("Trying to load again");
                    this.LoadRewardedVideo();
                }
            }, 0.5f);
        });
        this.rewardedVideoAd.RewardedVideoAdWillLogImpression = (delegate ()
        {
            Debug.Log("RewardedVideo ad logged impression.");
        });
        this.rewardedVideoAd.RewardedVideoAdDidClick = (delegate ()
        {
            Debug.Log("RewardedVideo ad clicked.");
        });

        this.rewardedVideoAd.RewardedVideoAdDidClose = (delegate ()
        {
            isLoading = false;
            closedWithSuccess = true;
            Debug.Log("Rewarded video ad did close.");
            CurrencyController.instance.AddCurrency(1);
            if (this.rewardedVideoAd != null)
            {
                this.rewardedVideoAd.Dispose();
                LoadRewardedVideo();
            }
        });

        this.rewardedVideoAd.rewardedVideoAdActivityDestroyed = (delegate () {
            isLoading = false;
            if (!this.closedWithSuccess)
            {
                Debug.Log("Rewarded video activity destroyed without being closed first.");
                Debug.Log("Game should resume. User should not get a reward.");
            }
        });


        // Initiate the request to load the ad.
        this.rewardedVideoAd.LoadAd();
    }

    public void ShowRewardedVideo()
    {
        if (isLoading)
            return;

        if (this.isLoaded)
        {
            this.isLoaded = false;

            try
            {
                this.rewardedVideoAd.Show();
            }
            catch (System.Exception)
            {
                LoadRewardedVideo();
            }

        }
        else
        {
            Debug.Log("Ad not loaded. Click load to request an ad.");
            LoadRewardedVideo();
        }
    }



    void OnDestroy()
    {
        // Dispose of interstitial ad when the scene is destroyed
        if (rewardedVideoAd != null)
        {
            rewardedVideoAd.Dispose();
        }
        Debug.Log("AdTest was destroyed!");
    }
}
