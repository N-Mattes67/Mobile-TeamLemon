using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public AudioSource soundFXSource;
    public AudioClip btnPressFX;

    private void Start()
    {
        bool soundfx = true;

        if (PlayerPrefs.HasKey("soundfx"))
        {
            soundfx = Convert.ToBoolean(PlayerPrefs.GetInt("soundfx"));
        }

        soundFXSource.enabled = soundfx;
    }

    public void PlayButtonFX()
    {
        if (soundFXSource.enabled) soundFXSource.PlayOneShot(btnPressFX);
    }
}
