using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LLMUnitySamples;

public class StareDetection : MonoBehaviour
{
    public float stareDurationThreshold = 3.0f;
    public float lookAwayDurationThreshold = 5.0f;
    public bool isStaring = false;
    [HideInInspector] public bool allTaskDone = false;
    private SimpleInteraction chatbotAI;
    private DomainExpansion domainExpansion;
    private GameObject playerCamera;

    private TaskHandler taskHandler;
    private TaskList taskList;
    private GameObject playerObject;
    private Coroutine domainCoroutine;

    private float stareTimer = 0.0f;
    private float lookAwayTimer = 0.0f;

    private void Start()
    {
        taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
        domainExpansion = GetComponent<DomainExpansion>();
        playerCamera = GameObject.Find("Main Camera");
        chatbotAI = GetComponent<SimpleInteraction>();
        taskList = GetComponent<TaskList>();
        playerObject = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerToTarget = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, playerToTarget);
        if (angle < 30f)
        {
            stareTimer += Time.deltaTime;
            if (stareTimer >= stareDurationThreshold && !isStaring && !allTaskDone)
            {
                eyeStare();
            }
        }
        else
        {
            if (domainExpansion.domainIsOn)
            {
                lookAwayTimer += Time.deltaTime;
                if(lookAwayTimer >= lookAwayDurationThreshold && isStaring)
                {
                    isStaring = false;
                    domainExpansion.domainIsOn = false;
                    stareTimer = 0.0f;
                    eyeClose();
                }
            }
        }
        if(allTaskDone && domainExpansion.domainIsOn)
        {
            isStaring = false;
            stareTimer = 0.0f;
            StartCoroutine(ReduceAnxietyOverTime(playerObject, 1f));
            this.enabled = false;
            eyeClose();
        }
        if (domainExpansion.domainIsOn)
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
        domainCoroutine = StartCoroutine(domainExpansion.Timestop(50, 5, true));
        isStaring = true;
        lookAwayTimer = 0.0f;
        taskHandler.stareDetection = this;
        domainExpansion.textToSpeechAI.LLM_Interaction = chatbotAI;
        domainExpansion.speechToTextAI.LLM_Interaction = chatbotAI;
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
        domainCoroutine = StartCoroutine(domainExpansion.Timestop(0, 3, false));
        taskHandler.taskUI.SetActive(false);
    }

    IEnumerator ReduceAnxietyOverTime(GameObject playerObject, float duration)
    {
        AnxietyMeter anxietyMeter = playerObject.GetComponent<AnxietyMeter>();
        float startValue = anxietyMeter.anxiety;
        float endValue = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            anxietyMeter.anxiety = Mathf.Lerp(startValue, endValue, t);
            yield return null;
        }

        anxietyMeter.anxiety = endValue;
    }

}
