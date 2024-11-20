using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueMeter : MonoBehaviour
{
    public Transform playerCamera;  // Reference to the player camera
    public float fatigueLevel = 0f; // Fatigue level, ranging from 0 (no fatigue) to 1 (maximum fatigue)
    public float fatigueIncreaseRate = 0.1f; // How quickly fatigue increases over time
    public float fatigueDecreaseRate = 0.3f; // How quickly fatigue decreases when holding the right mouse button
    public float maxFatigueTilt = 30f; // Maximum tilt angle when fatigue is at its highest
    public float punishmentTimeThreshold = 1f; // Time threshold before punishment is applied
    public float punishmentFatigueIncrease = 0.5f; // Fatigue increase when punishment is applied
    public Image blackScreen;
    public sanityMeter sanityMeter;
    public AudioClip sleepSFX;
    public subtitleScript subtitleScript;

    private float currentTilt = 0f; // Current tilt angle of the camera
    private float timeFacingDown = 0f; // Time the camera has been facing down
    private float fadeDuration = 2f;
    private float difficultyIncreaseCountdown = 120f;
    private bool sanityDecreased = false;

    void Update()
    {
        // Increase fatigue over time
        difficultyIncreaseCountdown -= Time.deltaTime;
        if(difficultyIncreaseCountdown <= 0)
        {
            difficultyIncreaseCountdown = 60f;
            fatigueIncreaseRate *= 2;
        }
        fatigueLevel += fatigueIncreaseRate * Time.deltaTime;
        Mathf.Clamp(fatigueLevel, 0, 10); // Clamp fatigue level between 0 and 1

        // Check if the right mouse button is held down
        if (Input.GetMouseButtonDown(1))
        {
            fatigueLevel -= fatigueDecreaseRate;
            fatigueLevel = Mathf.Clamp01(fatigueLevel); // Clamp fatigue level between 0 and 1
        }

        // Calculate the target tilt angle based on fatigue level
        float targetTilt = fatigueLevel * maxFatigueTilt;

        // Smoothly interpolate the current tilt towards the target tilt
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * 2f);

        // Apply the tilt to the camera
        playerCamera.localRotation = Quaternion.Euler(currentTilt, playerCamera.localRotation.eulerAngles.y, playerCamera.localRotation.eulerAngles.z);

        // Check if the camera is facing down
        if (currentTilt >= maxFatigueTilt * 0.9f)
        {
            timeFacingDown += Time.deltaTime;

            // Apply punishment if the camera has been facing down for too long
            if (timeFacingDown >= punishmentTimeThreshold)
            {
                if (!sanityDecreased)
                {
                    StartCoroutine(FadeOut());
                    sanityDecreased = true;
                }
            }
        }
        else
        {
            timeFacingDown = 0f; // Reset the timer if the camera is not facing down
        }
    }

    IEnumerator FadeOut()
    {
        AudioSource.PlayClipAtPoint(sleepSFX, gameObject.transform.position);
        float elapsedTime = 0f;
        blackScreen.color = Color.clear;
        blackScreen.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Calculate the new alpha value
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            // Update the image's alpha
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        // Ensure the image is fully opaque at the end of the fade-out
        blackScreen.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(3f);
        blackScreen.gameObject.SetActive(false);
        fatigueLevel = 0;
        timeFacingDown = 0f;
        fatigueIncreaseRate = 0.1f;
        sanityMeter.sanity -= 20;
        yield return new WaitForSeconds(3.3f);
        subtitleScript.subtitleOn();
        sanityDecreased = false;
    }
}
