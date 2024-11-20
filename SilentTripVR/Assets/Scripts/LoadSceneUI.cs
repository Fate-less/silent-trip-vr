using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneUI : MonoBehaviour
{
    public string sceneName;
    public int delayValue;
    public Animator anim;

    public void ButtonClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DelayedButtonClick(string sceneName)
    {
        anim.enabled = true;
        StartCoroutine(IntroDelay(sceneName));
    }

    IEnumerator IntroDelay(string sceneName)
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadBasedOnLevel()
    {
        Debug.Log(PlayerPrefs.GetString("currentLevel"));
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }
}
