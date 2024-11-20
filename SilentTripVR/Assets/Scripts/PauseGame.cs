using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool isOnPause = true;
    public GameObject playPauseObj;
    public Sprite pauseSprite;
    public Sprite playSprite;
    public GameObject tutorialScreen;

    private Image imgRenderer;

    private void Start()
    {
        imgRenderer = playPauseObj.GetComponent<Image>();
        Time.timeScale = 0;
        tutorialScreen.SetActive(true);
        AudioListener.pause = true;
    }

    public void PausePlayButton()
    {
        if (isOnPause)
        {
            isOnPause = false;
            imgRenderer.sprite = pauseSprite;
            Time.timeScale = 1;
            AudioListener.pause = false;
            tutorialScreen.SetActive(false);
        }
        else
        {
            isOnPause = true;
            imgRenderer.sprite = playSprite;
            Time.timeScale = 0;
            AudioListener.pause = true;
            tutorialScreen.SetActive(true);
        }
    }
}
