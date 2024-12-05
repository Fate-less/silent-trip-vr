using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;
using LLMUnitySamples;

public class SpeechToTextAI : MonoBehaviour
{
    private SpeechRecognizer recognizer;
    public SimpleInteraction LLM_Interaction;
    public TaskCompleteDetection LLM_Task;

    // Replace with your Azure for Students Speech API key and region
    private string speechKey = "9R9jmHFbcjc5izuChKI0lEgQ6Br3GBVhaZP6VJNSdCUa5XwnQwCwJQQJ99AKACqBBLyXJ3w3AAAYACOGCJNw";
    private string serviceRegion = "southeastasia";

    private bool isRecognizerInitialized = false;
    private bool isRecognitionActive = false;

    private bool speechRecognized = false;
    private string speechText;

    void Start()
    {
        LLM_Task = GameObject.Find("LLMTask").GetComponent<TaskCompleteDetection>();
        InitializeRecognizer();
    }

    private void InitializeRecognizer()
    {
        var config = SpeechConfig.FromSubscription(speechKey, serviceRegion);
        recognizer = new SpeechRecognizer(config);

        // Event: When speech is recognized
        recognizer.Recognized += (s, e) =>
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                Debug.Log($"Recognized: {e.Result.Text}");
                speechText = e.Result.Text;
                speechRecognized = true;
            }
        };

        // Event: If recognition is canceled
        recognizer.Canceled += (s, e) =>
        {
            Debug.LogError($"Canceled: {e.Reason}");
        };

        // Event: When the session stops
        recognizer.SessionStopped += (s, e) =>
        {
            Debug.Log("Session stopped.");
        };

        isRecognizerInitialized = true;
        Debug.Log("Speech recognizer initialized.");
    }

    void Update()
    {
        // Start recognition when the button is held down
        if (Input.GetButtonDown("Fire1") && isRecognizerInitialized && !isRecognitionActive)
        {
            StartCoroutine(StartRecognitionCoroutine());
        }

        // Stop recognition when the button is released
        if (Input.GetButtonUp("Fire1") && isRecognizerInitialized && isRecognitionActive)
        {
            StartCoroutine(StopRecognitionCoroutine());
        }
        if (speechRecognized)
        {
            OnSpeechRecognized(speechText);
            speechRecognized = false;
        }
    }

    private IEnumerator StartRecognitionCoroutine()
{
    var task = StartRecognitionAsync();
    while (!task.IsCompleted)
    {
        yield return null;
    }
    if (task.Exception != null)
    {
        Debug.LogError("Error starting recognition: " + task.Exception);
    }
}

private IEnumerator StopRecognitionCoroutine()
{
    var task = StopRecognitionAsync();
    while (!task.IsCompleted)
    {
        yield return null;
    }
    if (task.Exception != null)
    {
        Debug.LogError("Error stopping recognition: " + task.Exception);
    }
}


    private async Task StartRecognitionAsync()
    {
        if (recognizer != null && !isRecognitionActive)
        {
            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
            isRecognitionActive = true;
            Debug.Log("Speech recognition started.");
        }
    }

    private async Task StopRecognitionAsync()
    {
        if (recognizer != null && isRecognitionActive)
        {
            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            isRecognitionActive = false;
            Debug.Log("Speech recognition stopped.");
        }
    }

    private void OnSpeechRecognized(string recognizedText)
    {
        // Send the recognized text to Llama
        LLM_Interaction.onInputFieldSubmit(recognizedText);
        LLM_Task.onInputFieldSubmit(recognizedText);
        Debug.Log("Recognized text: " + recognizedText);
    }

    void OnDestroy()
    {
        if (recognizer != null)
        {
            recognizer.StopContinuousRecognitionAsync().Wait();
            recognizer.Dispose();
        }
    }
}
