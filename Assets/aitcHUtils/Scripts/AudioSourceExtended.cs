using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceExtended : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;

    public void PlayRandomClip() 
    {
        if (clips.Length <= 0) 
        {
            Debug.LogError("No Clip found. Please add atleast 1 sound clip");
            return;
        }

        int r = Random.Range(0, clips.Length);
        GetComponent<AudioSource>().clip = clips[r];
        GetComponent<AudioSource>().Play();
    }

    public void PlayClip(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    public void PlayRandomClip(AudioClip[] _clips)
    {
        if (_clips.Length <= 0)
        {
            Debug.LogError("No Clip found. Please add atleast 1 sound clip");
            return;
        }

        int r = Random.Range(0, _clips.Length);

        GetComponent<AudioSource>().clip = _clips[r];
        GetComponent<AudioSource>().Play();
    }
}
