using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LLMUnitySamples;

public class StareDetection : MonoBehaviour
{
    public float stareDurationThreshold = 3.0f;
    public bool isStaring = false;
    [HideInInspector] public bool allTaskDone = false;
    private SimpleInteraction chatbotAI;
    private DomainExpansion domainExpansion;
    private GameObject playerCamera;
    private bool domainIsOn = false;
    private SpeechToTextAI speechToTextAI;
    private TextToSpeechAI textToSpeechAI;
    private TaskHandler taskHandler;
    private TaskList taskList;
    private GameObject playerObject;
    private Coroutine domainCoroutine;

    private float stareTimer = 0.0f;

    private void Start()
    {
        taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
        domainExpansion = GetComponent<DomainExpansion>();
        playerCamera = GameObject.Find("Main Camera");
        chatbotAI = GetComponent<SimpleInteraction>();
        speechToTextAI = GameObject.Find("SpeechManager").GetComponent<SpeechToTextAI>();
        textToSpeechAI = GameObject.Find("TTSManager").GetComponent<TextToSpeechAI>();
        taskList = GetComponent<TaskList>();
        playerObject = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerToTarget = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, playerToTarget);
        if (angle < 45f)
        {
            stareTimer += Time.deltaTime;
            if (stareTimer >= stareDurationThreshold && !isStaring && !allTaskDone)
            {
                eyeStare();
            }
        }
        else
        {
            if (domainIsOn)
            {
                isStaring = false;
                domainIsOn = false;
                stareTimer = 0.0f;
                eyeClose();
            }
        }
        if(allTaskDone && domainIsOn)
        {
            isStaring = false;
            domainIsOn = false;
            stareTimer = 0.0f;
            eyeClose();
        }
        if (domainIsOn)
        {
            playerObject.GetComponent<AnxietyMeter>().anxiety += 0.005f * Time.deltaTime;
        }
    }

    public void eyeStare()
    {
        if (domainCoroutine != null)
        {
            StopCoroutine(domainCoroutine);
            domainCoroutine = null;
        }
        domainCoroutine = StartCoroutine(domainExpansion.Timestop(50, 5));
        isStaring = true;
        domainIsOn = true;
        taskHandler.stareDetection = this;
        textToSpeechAI.LLM_Interaction = chatbotAI;
        speechToTextAI.LLM_Interaction = chatbotAI;
        speechToTextAI.InitializeRecognizer();
        taskHandler.taskUI.SetActive(true);
        taskList.DisplayTask();
    }

    public void eyeClose()
    {
        if (domainCoroutine!=null)
        {
            StopCoroutine(domainCoroutine);
            domainCoroutine = null;
        }
        domainCoroutine = StartCoroutine(domainExpansion.Timestop(0, 3));
        taskHandler.taskUI.SetActive(false);
        if (speechToTextAI.isRecognizerInitialized && speechToTextAI.isRecognitionActive)
        {
            speechToTextAI.recognizer.StopContinuousRecognitionAsync().Wait();
        }
    }
}
