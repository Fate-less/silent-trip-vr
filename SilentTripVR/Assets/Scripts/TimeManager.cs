using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimeManager : MonoBehaviour
{
    public float duration = 300f;
    public bool passed260 = false;
    public bool passed220 = false;
    public bool passed140 = false;
    public TextMeshProUGUI timeTMP;

    private int min;
    private int time = 7;
    private Color originalColor;

    public EventCondition eventHandler;
    public GadgetInteract gadgetInteract;
    public Image blackScreen;
    public float fadeDuration = 2.0f;
    public AudioClip trainArrive;
    private bool audioPlayed = false;
    private bool audioPlayed2 = false;
    private bool audioPlayed3 = false;
    public PlayableDirector director;
    public AudioClip playerArrive;

    private void Start()
    {
        originalColor = blackScreen.color;
        blackScreen.color = Color.clear;
        PlayerPrefs.SetString("currentLevel", SceneManager.GetActiveScene().name);
        Debug.Log(PlayerPrefs.GetString("currentLevel"));
    }
    // Update is called once per frame
    void Update()
    {
        duration = duration - Time.deltaTime;
        min = Mathf.FloorToInt(duration / 60);
        if(min == 4)
        {
            if(SceneManager.GetActiveScene().name == "PlaneGame")
            {
                time = 7;
                timeTMP.text = time + " PM";
            }
            else
            {
                time = 3;
                timeTMP.text = time + " PM";
            }
        }
        else if(min == 2)
        {
            if (SceneManager.GetActiveScene().name == "PlaneGame")
            {
                time = 2;
                timeTMP.text = time + " AM";
            }
            else
            {
                time = 4;
                timeTMP.text = time + " PM";
            }
        }
        else if(min == 1)
        {
            if (SceneManager.GetActiveScene().name == "PlaneGame")
            {
                time = 9;
                timeTMP.text = time + " AM";
            }
            else
            {
                time = 5;
                timeTMP.text = time + " PM";
            }
        }
        if (duration <= 0)
        {
            blackScreen.gameObject.SetActive(true);
            if (!audioPlayed)
            {
                AudioSource.PlayClipAtPoint(trainArrive, gameObject.transform.position);
                audioPlayed = true;
            }
            Debug.Log("start fading out");
            StartCoroutine(FadeOut());
        }
        else if (duration <= 140 && !passed140)
        {
            eventHandler.ManAsk();
            if (gadgetInteract.isOpen)
            {
                gadgetInteract.CloseGadget();
            }
            passed140 = true;
        }
        else if (duration <= 220 && !passed220)
        {
            eventHandler.WomanAsk();
            if (gadgetInteract.isOpen)
            {
                gadgetInteract.CloseGadget();
            }
            passed220 = true;
        }
        else if (duration <= 260 && !passed260)
        {
            if(director != null)
            {
                director.Play();
            }
            passed260 = true;
        }
    }

    IEnumerator transitionDelay()
    {
        if (SceneManager.GetActiveScene().name == "PlaneGame")
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("MamarikaCutscene");
        }
        else
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("PlaneCutscene");
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Calculate the new alpha value
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            // Update the image's alpha
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        if(SceneManager.GetActiveScene().name == "TrainGame")
        {
            NPCHandler npcHandler = gameObject.GetComponent<NPCHandler>();
            for (int i = 0; i < npcHandler.npcList.Count; i++)
            {
                npcHandler.npcList[i].SetActive(false);
            }
        }
        // Ensure the image is fully opaque at the end of the fade-out
        blackScreen.color = new Color(0, 0, 0, 1);
        AudioListener.volume = 0;
        StartCoroutine(transitionDelay());
    }
}
