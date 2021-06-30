using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    [Tooltip("Object with the audio source")][SerializeField] AudioSource audioPlayer;

    public void Anim_PlayClip(AudioClip clip) 
    {
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }
}
