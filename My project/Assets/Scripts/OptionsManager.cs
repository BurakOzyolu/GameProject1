using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] Toggle muteToggle;
    [SerializeField] Toggle windowedToggle;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeText;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Mute"))
        {
            PlayerPrefs.SetInt("Mute",0);
        }
        else
        {
            LoadMuteToggle();
        }
        if (!PlayerPrefs.HasKey("Windowed"))
        {
            PlayerPrefs.SetInt("Windowed", 0);
        }
        else
        {
            LoadWindowedToggle();
        }
    }
    private void Update()
    {
        LoadVolume();
    }

    private void LoadMuteToggle()
    {
        muteToggle.isOn = PlayerPrefs.GetInt("Mute") == 1;
    }

    private void LoadWindowedToggle()
    {
        windowedToggle.isOn = PlayerPrefs.GetInt("Windowed") == 1;
    }

    public void MuteToggle()
    {
        PlayerPrefs.SetInt("Mute",muteToggle.isOn ? 1 : 0);
        if (muteToggle.isOn)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }
    public void WindowedToggle()
    {
        PlayerPrefs.SetInt("Windowed", windowedToggle.isOn ? 1 : 0);
        if (!windowedToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
    private void LoadVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
    public void VolumeControl(float volume)
    {
        volumeText.text = "Volume "+ volume.ToString("0");
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeValue);
    }
}
