using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainExpansion : MonoBehaviour
{
    private ShaderInteractor shaderInteractor;

    private void Start()
    {
        shaderInteractor = GetComponent<ShaderInteractor>();
    }

    public IEnumerator Timestop(float endValue, float duration)
    {
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
    }
}
