using Firebase.Messaging;
using System;
using UnityEngine;


public class FirebaseAnalyticController : MonoBehaviourSingleton<FirebaseAnalyticController>
{
    public GUISkin fb_GUISkin;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;

    [HideInInspector]
    public bool isLevelPlayLogged = false;


    // Start is called before the first frame update
    private void Start()
    {
        //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        //Debug.LogError("Line No: 43");
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.LogError(
                  "Resolved all Firebase dependencies: " + dependencyStatus);
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // Setup message event handlers.
    void InitializeFirebase()
    {
        Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //FirebaseMessaging.SubscribeAsync("text");
        //Debug.LogError("Line No: 43");
        //Debug.Log("Line No: 45");

        aitcHUtils.MiscUtils.DoWithDelay(this, () =>
         {

         }, 10f);
        //Firebase.Messaging.FirebaseMessaging.SubscribeAsync(topic).ContinueWithOnMainThread(task => {
        //    //LogTaskCompletion(task, "SubscribeAsync");
        //});
        //DebugLog("Firebase Messaging Initialized");

        // This will display the prompt to request permission to receive
        // notifications if the prompt has not already been displayed before. (If
        // the user already responded to the prompt, thier decision is cached by
        // the OS and can be changed in the OS settings).
        //Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
        //  task => {
        //      LogTaskCompletion(task, "RequestPermissionAsync");
        //  }
        //);
        isFirebaseInitialized = true;
    }

    void temp()
    {
    }

    public void LogLevel(string levelNumber)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(levelNumber);
        Debug.Log("Event Logged");
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {

    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {

    }
}
