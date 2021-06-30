using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBAppEventController : MonoBehaviour
{

    void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() =>
            {
                FB.ActivateApp();
            });
        }
    }

    //void OnApplicationPause(bool pauseStatus)
    //{
    //    // Check the pauseStatus to see if we are in the foreground
    //    // or background
    //    if (!pauseStatus)
    //    {
    //        //app resume
    //        if (FB.IsInitialized)
    //        {
    //            FB.ActivateApp();
    //        }
    //        else
    //        {
    //            //Handle FB.Init
    //            FB.Init(() => {
    //                FB.ActivateApp();
    //            });
    //        }
    //    }
    //}

    /**
* Include the Facebook namespace via the following code:
* using Facebook.Unity;
*
* For more details, please take a look at:
* developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
*/
    public void LogTest_eventEvent()
    {
        FB.LogAppEvent(
            "test_event"
        );

        Debug.Log("Event logged");
    }

    public void LogAchievedLevelEvent(string level)
    {
        var parameters = new Dictionary<string, object>();
        parameters[AppEventParameterName.Level] = level;
        FB.LogAppEvent(AppEventName.AchievedLevel, null, parameters);
    }
}
