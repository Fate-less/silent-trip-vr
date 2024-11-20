using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pesawatTutorial : MonoBehaviour
{
    public GameObject[] cutsceneImage;
    public int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (index < 1)
            {
                cutsceneImage[index].SetActive(false);
                index++;
                cutsceneImage[index].SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("PlaneGame");
            }
        }
    }
}
