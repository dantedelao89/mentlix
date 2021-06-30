using aitcHUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviourSingleton<SoundController>
{
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer sfxMixer;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;

    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;

    private void Start()
    {
        PlayerMusic();
    }

    public void toggle_MusicVolume()
    {
        musicMixer.SetFloat("musicVol", MiscUtils.ValueToVolume(musicToggle.isOn ? 1 : 0, 0));
    }

    public void toggle_SoundVolume()
    {
        sfxMixer.SetFloat("sfxVol", MiscUtils.ValueToVolume(soundToggle.isOn ? 1 : 0, 0));
    }

    public void PlayerSFX(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayerMusic()
    {
        musicSource.Play();
    }
}
