using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using LLMUnitySamples;

public class TextToSpeechAI : MonoBehaviour
{
    private string speechKey = "9R9jmHFbcjc5izuChKI0lEgQ6Br3GBVhaZP6VJNSdCUa5XwnQwCwJQQJ99AKACqBBLyXJ3w3AAAYACOGCJNw";
    private string serviceRegion = "southeastasia";

    private SpeechSynthesizer synthesizer;
    private TaskHandler taskHandler;
    public SimpleInteraction LLM_Interaction;
    public SpeechToTextAI speechToTextAI;

    private void Start()
    {
        var config = SpeechConfig.FromSubscription(speechKey, serviceRegion);
        synthesizer = new SpeechSynthesizer(config);
        taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
    }

    public async void SpeakResponse(string responseText)
    {
        if (string.IsNullOrWhiteSpace(responseText))
        {
            Debug.LogWarning("No text provided for speech synthesis.");
            return;
        }

        try
        {
            Debug.Log("Speaking: " + responseText);
            await speechToTextAI.StopRecognitionAsync();
            // Perform speech synthesis
            using (var result = await synthesizer.SpeakTextAsync(responseText))
            {
                if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    Debug.Log("Speech synthesis succeeded.");
                    if (!taskHandler.stareDetection.allTaskDone)
                    {
                        await speechToTextAI.StartRecognitionAsync();
                    }
                    LLM_Interaction.HideText();
                }
                else
                {
                    Debug.LogError($"Speech synthesis failed. Reason: {result.Reason}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred during speech synthesis: " + ex.Message);
        }
    }

    private void OnDestroy()
    {
        // Dispose the synthesizer when the script is destroyed
        synthesizer?.Dispose();
    }
}

