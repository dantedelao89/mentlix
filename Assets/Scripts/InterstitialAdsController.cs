//using UnityEngine;
//using UnityEngine.UI;
//using AudienceNetwork;
//using UnityEngine.SceneManagement;
//using AudienceNetwork.Utility;

//public class InterstitialAdsController : MonoBehaviourSingleton<InterstitialAdsController>
//{
//    [SerializeField]
//    public bool ShowTestAds;
//    [SerializeField]
//    private string interstitialId;

//    private InterstitialAd interstitialAd;
//    private bool isLoaded;
//#pragma warning disable 0414
//    private bool didClose;


//#pragma warning restore 0414
//    // UI elements in scene
//    //public Text statusLabel;

//    public new void Awake()
//    {
//        AdSettings.AddTestDevice(SystemInfo.deviceUniqueIdentifier);
//        //AdSettings.AddTestDevice("198614aaf2dabf41d6175a64d4dfc0c7");
//        //AdSettings.AddTestDevice("c2a2f49e6e0c15095bd5569496236295");
//        //AdSettings.AddTestDevice(SystemInfo.deviceUniqueIdentifier);
//        AudienceNetworkAds.Initialize();
//        SettingsScene.InitializeSettings();
//    }

//    private void Start()
//    {
//        if (ShowTestAds)
//            interstitialId = "IMG_16_9_APP_INSTALL#" + interstitialId;

//        if (AudienceNetworkAds.IsInitialized())
//            LoadInterstitial();
//        else
//            AudienceNetworkAds.Initialize();
//    }

//    // Load button
//    public void LoadInterstitial()
//    {
//        //statusLabel.text = "Loading ad...";

//        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
//        // Use different ID for each ad placement in your app.
//        interstitialAd = new InterstitialAd(interstitialId);

//        interstitialAd.Register(gameObject);

//        // Set delegates to get notified on changes or when the user interacts with the ad.
//        interstitialAd.InterstitialAdDidLoad = delegate ()
//        {
//            Debug.LogError("Ad loaded.");
//            isLoaded = true;
//            didClose = false;
//            string isAdValid = interstitialAd.IsValid() ? "valid" : "invalid";
//            //statusLabel.text = "Ad loaded and is " + isAdValid + ". Click show to present!";
//        };
//        interstitialAd.InterstitialAdDidFailWithError = delegate (string error)
//        {
//            Debug.LogError("Ad failed to load with error: " + error + " Id : " + SystemInfo.deviceUniqueIdentifier );
//            LoadInterstitial();
//            //statusLabel.text = "Error: " + error + ". Please add your device id for testing. Device Id: " + SystemInfo.deviceUniqueIdentifier;
//        };
//        interstitialAd.InterstitialAdWillLogImpression = delegate ()
//        {
//            Debug.LogError("Ad logged impression.");
//        };
//        interstitialAd.InterstitialAdDidClick = delegate ()
//        {
//            Debug.Log("Ad clicked.");
//        };
//        interstitialAd.InterstitialAdDidClose = delegate ()
//        {
//            Debug.Log("Interstitial ad did close.");
//            didClose = true;
//            if (interstitialAd != null)
//            {
//                interstitialAd.Dispose();
//                LoadInterstitial();
//            }
//        };

//#if UNITY_ANDROID
//        /*
//         * Only relevant to Android.
//         * This callback will only be triggered if the Interstitial activity has
//         * been destroyed without being properly closed. This can happen if an
//         * app with launchMode:singleTask (such as a Unity game) goes to
//         * background and is then relaunched by tapping the icon.
//         */
//        interstitialAd.interstitialAdActivityDestroyed = delegate () {
//            if (!didClose)
//            {
//                Debug.Log("activity destroyed without being closed first.");
//                Debug.Log("Game should resume.");
//            }
//        };
//#endif

//        // Initiate the request to load the ad.
//        interstitialAd.LoadAd();
//    }

//    // Show button
//    public void ShowInterstitial()
//    {
//        if (isLoaded)
//        {
//            interstitialAd.Show();
//            isLoaded = false;
//        }
//        else
//        {
//            //statusLabel.text = "Ad not loaded. Click load to request an ad.";
//            LoadInterstitial();
//        }
//    }

//    void OnDestroy()
//    {
//        // Dispose of interstitial ad when the scene is destroyed
//        if (interstitialAd != null)
//        {
//            interstitialAd.Dispose();
//        }
//        Debug.Log("AdTest was destroyed!");
//    }
//}
