using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundFXSource;
    public AudioSource musicSource;
    public AudioClip btnPressFX;

    public Toggle soundFXToggle, musicToggle, vibrationToggle;

    public bool vibrate = true;

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
        if (PlayerPrefs.HasKey("vibrate"))
        {
            vibrate = Convert.ToBoolean(PlayerPrefs.GetInt("vibrate"));
        }

        soundFXSource.enabled = soundfx;
        musicSource.enabled = music;

        soundFXToggle.isOn = soundfx;
        musicToggle.isOn = music;
        vibrationToggle.isOn = vibrate;
    }

    public void PlayButtonFX()
    {
        if (soundFXSource.enabled) soundFXSource.PlayOneShot(btnPressFX);
    }

    public void ToggleSoundFX()
    {
        soundFXSource.enabled = !soundFXSource.enabled;
        PlayerPrefs.SetInt("soundfx", Convert.ToInt32(soundFXSource.enabled));
        PlayerPrefs.Save();
    }

    public void ToggleMusic()
    {
        musicSource.enabled = !musicSource.enabled;
        PlayerPrefs.SetInt("music", Convert.ToInt32(musicSource.enabled));
        PlayerPrefs.Save();
    }

    public void ToggleVibration()
    {
       vibrate = !vibrate;
       PlayerPrefs.SetInt("vibrate", Convert.ToInt32(vibrate));
       PlayerPrefs.Save();
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}