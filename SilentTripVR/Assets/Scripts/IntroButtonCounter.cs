using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroButtonCounter : MonoBehaviour, IPointerClickHandler
{
    public int clickCounter;
    public AudioSource audioSource;
    public AudioClip trainSFX;
    private float resetDuration;

    public GameObject trainObj;
    public AudioSource trainSource;
    public AudioClip trainArriveSFX;
    private bool sfxPlayed = false;

    private Button startButton;
    private TextMeshProUGUI playButtonText;
    private Image playButtonImage;
    private GameObject MenuBGM;

    public GameObject blackOut;
    public GameObject titleImage;
    public AudioClip titleSFX;
    private bool blackOutStart = false;
    public TextMeshProUGUI instruction;

    private void Start()
    {
        startButton = gameObject.GetComponent<Button>();
        playButtonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        playButtonImage = transform.GetChild(1).GetComponent<Image>();
    }

    public void CountUp()
    {
        audioSource.PlayOneShot(trainSFX);
        clickCounter++;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CountUp();
    }

    private void Update()
    {
        if (blackOutStart)
        {
            blackOut.GetComponent<Image>().fillAmount = blackOut.GetComponent<Image>().fillAmount + Time.deltaTime * 5;
        }
        resetDuration = resetDuration + Time.deltaTime;
        if(resetDuration >= 3)
        {
            clickCounter = 0;
            resetDuration = 0;
        }
        if(clickCounter >= 6 && !sfxPlayed)
        {
            trainSource.PlayOneShot(trainArriveSFX);
            sfxPlayed = true;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
            startButton.interactable = false;
            startButton.transition = Selectable.Transition.None;
            instruction.gameObject.SetActive(false);
            StartCoroutine(TrainAnimation());
        }
    }

    IEnumerator TrainAnimation()
    {
        try
        {
            MenuBGM = GameObject.Find("MenuBGM");
            Destroy(MenuBGM);
        }
        catch { }
        yield return new WaitForSeconds(6f);
        trainObj.SetActive(true);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.6f);
        playButtonText.enabled = false;
        playButtonImage.enabled = false;
        gameObject.GetComponent<Image>().color = Color.clear;
        yield return new WaitForSeconds(6f);
        audioSource.PlayOneShot(titleSFX);
        yield return new WaitForSeconds(0.5f);
        blackOut.SetActive(true);
        blackOutStart = true;
        yield return new WaitForSeconds(2.8f);
        titleImage.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("tutorialKereta");
    }

}
