using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundFXSource;
    public AudioSource musicSource;

    public AudioClip coinCollectionFX;
    public AudioClip buttonClickFX;
    public AudioClip onDeathFX;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bool soundfx = true;
        bool music = true;

        if (PlayerPrefs.HasKey("soundfx"))
        {
            soundfx = Convert.ToBoolean(PlayerPrefs.GetInt("soundfx"));
        }
        if (PlayerPrefs.HasKey("music"))
        {
            music = Convert.ToBoolean(PlayerPrefs.GetInt("music"));
        }

        soundFXSource.enabled = soundfx;
        musicSource.enabled = music;
    }

    public static AudioManager Instance { get; private set; }

    public void PlayCoinFX()
    {
        if (soundFXSource.enabled)
        {
            soundFXSource.PlayOneShot(coinCollectionFX);
        }
        
    }

    public void PlayButtonClickFX()
    {
        if (soundFXSource.enabled)
        {
            soundFXSource.PlayOneShot(buttonClickFX);
        }
    }

    public void PlayDeathFX()
    {
        if (soundFXSource.enabled)
        {
            soundFXSource.PlayOneShot(onDeathFX);
        }   
    }
}
