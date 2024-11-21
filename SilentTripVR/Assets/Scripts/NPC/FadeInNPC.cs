using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInNPC : MonoBehaviour
{
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    public IEnumerator SpriteFade(SpriteRenderer sr, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = sr.color.a;

        Debug.Log("Fade Initiating");

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);

            Debug.Log("Fade On Proc");

            // Yield until the next frame
            yield return null;
        }

        // Ensure final alpha value is set after the loop completes
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, endValue);

        Debug.Log("Fade Success");
    }
}
