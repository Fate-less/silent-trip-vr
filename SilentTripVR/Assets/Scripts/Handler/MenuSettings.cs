using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Slider volumeSlider;


    void Start()
    {
        if(SceneManager.GetActiveScene().name == "PengaturanScene")
        {
            LoadSettings();
        }
        else
        {
            AllSceneSettings();
        }
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVolume(float volume)
    {
        volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        AudioListener.volume = volume;
    }

    public void LoadSettings()
    {
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        SetFullscreen(isFullscreen);

        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SetVolume(volume);
    }
    public void SaveSettings()
    {
        int isFullscreen = fullscreenToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen);

        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);

        PlayerPrefs.Save();
    }

    public void AllSceneSettings()
    {
        //bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        //SetFullscreen(isFullscreen);
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = volume;
    }
}
