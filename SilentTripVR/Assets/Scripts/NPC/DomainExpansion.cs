using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainExpansion : MonoBehaviour
{
    private ShaderInteractor shaderInteractor;
    public bool domainIsOn = false;
    [HideInInspector] public SpeechToTextAI speechToTextAI;
    [HideInInspector] public TextToSpeechAI textToSpeechAI;

    private void Start()
    {
        shaderInteractor = GetComponent<ShaderInteractor>();
        speechToTextAI = GameObject.Find("SpeechManager").GetComponent<SpeechToTextAI>();
        textToSpeechAI = GameObject.Find("TTSManager").GetComponent<TextToSpeechAI>();
    }

    public IEnumerator Timestop(float endValue, float duration, bool activate)
    {
        if(shaderInteractor.enabled == false)
        {
            shaderInteractor.enabled = true;
        }
        float elapsedTime = 0;
        float startValue = shaderInteractor.radius;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newArea = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            shaderInteractor.radius = newArea;
            yield return null;
        }

        // Ensure final alpha value is set after the loop completes
        shaderInteractor.radius = endValue;

        Debug.Log("Time stopped...");
        domainIsOn = true;
        shaderInteractor.enabled = false;
        if (activate)
        {
            TurnOnRecognition();
        }
        else
        {
            TurnOffRecognition();
        }
    }

    public async void TurnOnRecognition()
    {
        await speechToTextAI.StartRecognitionAsync();
    }
    public async void TurnOffRecognition()
    {
        await speechToTextAI.StopRecognitionAsync();
    }
}
