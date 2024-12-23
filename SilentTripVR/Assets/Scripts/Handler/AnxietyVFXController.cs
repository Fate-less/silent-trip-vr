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
    [SerializeField] private AnxietyMeter anxietyMeter;
    private Material material;
    private float startTime;

    private void Start()
    {
        if (TryGetFeature(out var feature))
        {
            var blitFeature = feature as Blit;
            material = blitFeature.settings.blitMaterial;
        }
    }
    void Update()
    {
        material.SetFloat("_FullscreenIntensity", anxietyMeter.anxiety);
    }

    private void OnDestroy()
    {
        material.SetFloat("_FullscreenIntensity", 0);
    }

    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        return feature != null;
    }
}
