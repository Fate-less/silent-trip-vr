using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigameTutorial : MonoBehaviour
{
    public GameObject minigameParentScene;
    public GameObject[] cutsceneImage;
    public int index = 0;

    public void OpenMinigameTutorial()
    {
        int doneTutorial = PlayerPrefs.GetInt("Minigame Tutorial", 0);
        if(doneTutorial == 0)
        {
            minigameParentScene.SetActive(true);
            cutsceneImage[0].SetActive(true);
            PlayerPrefs.SetInt("Minigame Tutorial", 1);
            Time.timeScale = 0f;
        }
        else { }
    }

    private void Update()
    {
        if (minigameParentScene.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (index < 3)
                {
                    cutsceneImage[index].SetActive(false);
                    index++;
                    cutsceneImage[index].SetActive(true);
                }
                else
                {
                    cutsceneImage[index].SetActive(false);
                    minigameParentScene.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
