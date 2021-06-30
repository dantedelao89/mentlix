using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    VideoPlayer vidPlayer;
    bool videoPlayed = false;

    void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
        //vidPlayer.Play();
        //Call the LoadButton() function when the user clicks this Button
        StartCoroutine(StartGame());
        vidPlayer.loopPointReached += VideoEnded;
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video Is Over");
        videoPlayed = true;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            //Output the current progress

            // Check if the load has finished
            if (operation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                //Wait to you press the space key to activate the Scene
                //if ((ulong)vidPlayer.frame == vidPlayer.clip.frameCount - 1)
                if (videoPlayed)
                    operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    //IEnumerator LoadScene()
    //{
    //    yield return null;

    //    //Begin to load the Scene you specify
    //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
    //    //Don't let the Scene activate until you allow it to
    //    asyncOperation.allowSceneActivation = false;
    //    Debug.Log("Pro :" + asyncOperation.progress);
    //    //When the load is still in progress, output the Text and progress bar
    //    while (!asyncOperation.isDone)
    //    {
    //        //Output the current progress

    //        // Check if the load has finished
    //        if (asyncOperation.progress >= 0.9f)
    //        {
    //            Debug.Log("t: " + vidPlayer.clip.frameCount + " | " + (ulong)vidPlayer.frame);
    //            //Change the Text to show the Scene is ready
    //            //Wait to you press the space key to activate the Scene
    //            if ((ulong)vidPlayer.frame  == vidPlayer.clip.frameCount - 1)
    //                asyncOperation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
}