using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cyan;

public class AnxietyVFXController : MonoBehaviour
{
    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    [SerializeField] private float transitionPeriod = 1;

    private bool transitioning;
    private float startTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            StartTransition();
        }
        if (transitioning)
        {
            if(Time.timeSinceLevelLoad >= startTime * transitionPeriod)
            {
                EndTransition();
            }
            else
            {
                UpdateTransition();
            }
        }
    }

    private void OnDestroy()
    {
        ResetTransition();
    }

    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        return feature != null;
    }
    public void StartTransition()
    {
        startTime = Time.timeSinceLevelLoad;
        transitioning = true;
    }
    private void UpdateTransition()
    {
        if(TryGetFeature(out var feature))
        {
            float intensity = Mathf.Clamp01((Time.timeSinceLevelLoad - startTime) / transitionPeriod);

            var blitFeature = feature as Blit;
            var material = blitFeature.settings.blitMaterial;
            material.SetFloat("FullscreenIntensity", intensity);
        }
    }

    private void EndTransition()
    {
        if(TryGetFeature(out var feature))
        {
            feature.SetActive(false);
            rendererData.SetDirty();
            transitioning = false;
        }
    }

    private void ResetTransition()
    {
        if(TryGetFeature(out var feature))
        {
            feature.SetActive(true);
            rendererData.SetDirty();

            var blitFeature = feature as Blit;
            var material = blitFeature.settings.blitMaterial;
            material.SetFloat("FullscreenIntensity", 0);

            transitioning = false;
        }
    }
}
