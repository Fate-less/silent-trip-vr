using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LLMUnitySamples;

public class StareDetection : MonoBehaviour
{
    public float stareDurationThreshold = 3.0f;
    public bool isStaring = false;
    private SimpleInteraction chatbotAI;
    private DomainExpansion domainExpansion;
    private GameObject playerCamera;
    private bool domainIsOn = false;
    private SpeechToTextAI speechToTextAI;
    private TextToSpeechAI textToSpeechAI;
    private TaskHandler taskHandler;

    private float stareTimer = 0.0f;

    private void Start()
    {
        taskHandler = GetComponent<TaskHandler>();
        domainExpansion = GetComponent<DomainExpansion>();
        playerCamera = GameObject.Find("Main Camera");
        chatbotAI = GetComponent<SimpleInteraction>();
        speechToTextAI = GameObject.Find("SpeechManager").GetComponent<SpeechToTextAI>();
        textToSpeechAI = GameObject.Find("TTSManager").GetComponent<TextToSpeechAI>();
    }

    void Update()
    {
        Vector3 playerToTarget = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, playerToTarget);
        if (angle < 45f)
        {
            stareTimer += Time.deltaTime;
            if (stareTimer >= stareDurationThreshold && !isStaring)
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
    }

    public void eyeStare()
    {
        StartCoroutine(domainExpansion.Timestop(50, 10));
        isStaring = true;
        domainIsOn = true;
        taskHandler.stareDetection = this;
        textToSpeechAI.LLM_Interaction = chatbotAI;
        speechToTextAI.LLM_Interaction = chatbotAI;
        StartCoroutine(speechToTextAI.StartRecognitionCoroutine());
    }

    public void eyeClose()
    {
        StartCoroutine(domainExpansion.Timestop(0, 5));
        StartCoroutine(speechToTextAI.StopRecognitionCoroutine());
    }
}
