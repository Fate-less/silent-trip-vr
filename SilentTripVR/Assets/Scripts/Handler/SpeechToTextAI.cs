using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class SpeechToTextAI : MonoBehaviour
{
    private SpeechRecognizer recognizer;

    // Replace with your Azure for Students Speech API key and region
    private string speechKey = "9R9jmHFbcjc5izuChKI0lEgQ6Br3GBVhaZP6VJNSdCUa5XwnQwCwJQQJ99AKACqBBLyXJ3w3AAAYACOGCJNw";
    private string serviceRegion = "southeastasia";

    void Start()
    {
        InitializeRecognizer();
        OnSpeechRecognized("this is testing, please answer");
    }

    private async void InitializeRecognizer()
    {
        var config = SpeechConfig.FromSubscription(speechKey, serviceRegion);
        recognizer = new SpeechRecognizer(config);

        // Event: When speech is recognized
        recognizer.Recognized += (s, e) =>
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                Debug.Log($"Recognized: {e.Result.Text}");
                OnSpeechRecognized(e.Result.Text);
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

        await StartRecognitionAsync();
    }

    private async Task StartRecognitionAsync()
    {
        await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
        Debug.Log("Speech recognition started.");
    }

    private void OnSpeechRecognized(string recognizedText)
    {
        // Send the recognized text to ChatGPT
        Debug.Log("Recognized text: " + recognizedText);
    }


    void OnDestroy()
    {
        recognizer.StopContinuousRecognitionAsync().Wait();
        recognizer.Dispose();
    }
}
