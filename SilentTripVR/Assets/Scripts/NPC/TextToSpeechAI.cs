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
    public SimpleInteraction LLM_Interaction;

    private void Start()
    {
        var config = SpeechConfig.FromSubscription(speechKey, serviceRegion);
        synthesizer = new SpeechSynthesizer(config);
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

            // Perform speech synthesis
            using (var result = await synthesizer.SpeakTextAsync(responseText))
            {
                if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    Debug.Log("Speech synthesis succeeded.");
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

