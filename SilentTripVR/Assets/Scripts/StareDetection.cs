using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StareDetection : MonoBehaviour
{
    public float stareDurationThreshold = 3.0f; // Duration threshold for triggering the event
    public GameObject playerCamera; // Reference to the player's camera
    public bool isStaring = false; // Flag to track if the player is staring at the object
    public CameraZoom cameraScript;
    public GameObject eyeObject;
    public AudioSource playerAudio;
    public AudioClip heartbeatSFX;
    public playerSanity anxietyMeter;
    public GadgetInteract gadgetInteract;
    public AudioHandler audioHandler;
    public NoddingScript nodScript;
    public EventCondition eventManager;
    public TextMeshProUGUI subtitleTMP;


    private float stareTimer = 0.0f;

    void Update()
    {
        // Check if the player is looking at the object
        Vector3 playerToTarget = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, playerToTarget);
        if (angle < 30f && !gadgetInteract.isOpen && !nodScript.isNodding && !nodScript.isShaking && !eventManager.isAsking) // Adjust this angle to define the threshold for "staring"
        {
            // Increment the stare timer if the player is looking at the object
            stareTimer += Time.deltaTime;
            if(cameraScript.lastStarePerson == gameObject)
            {
                anxietyMeter.anxietyIncrease(5f);
            }

            // Trigger the event if the stare duration exceeds the threshold
            if (stareTimer >= stareDurationThreshold && !isStaring)
            {
                eyeStare();
            }
        }
        else
        {
            // Reset the stare timer if the player looks away from the object
            if (eyeObject.activeInHierarchy)
            {
                isStaring = false;
                eyeObject.GetComponent<Animator>().SetBool("isStaring", false);
                eyeObject.GetComponent<Animator>().Play("EyeClose");
                stareTimer = 0.0f;
                StartCoroutine(eyeClose());
            }
        }
        try
        {
            if (!cameraScript.lastStarePerson.GetComponent<StareDetection>().isStaring)
            {
                cameraScript.isAssured = true;
                anxietyMeter.multiplier = 1;
                anxietyMeter.decrementValue = 5f;
            }
        }
        catch { }
    }

    public void eyeStare()
    {
        eyeObject.SetActive(true);
        eyeObject.GetComponent<Animator>().SetBool("isStaring", true);
        eyeObject.GetComponent<Animator>().Play("EyeOpen");
        isStaring = true;
        cameraScript.isAssured = false;
        cameraScript.lastStarePerson = gameObject;
        anxietyMeter.multiplier = anxietyMeter.multiplier + 0.2f;
        cameraScript.LookAtObject(eyeObject);
        cameraScript.ZoomToObject(gameObject.transform);
        StartCoroutine(cameraScript.RotateBack());
        playerAudio.PlayOneShot(heartbeatSFX);
        audioHandler.PlaySpottedSound();
        subtitleTMP.text = "Oh no, that person is staring at me";
        subtitleTMP.transform.parent.gameObject.SetActive(false);
        StartCoroutine(subtitleAppear());
    }

    public IEnumerator eyeClose()
    {
        yield return new WaitForSeconds(2f);
        eyeObject.SetActive(false);
    }

    IEnumerator subtitleAppear()
    {
        subtitleTMP.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        if (subtitleTMP.transform.parent.gameObject.activeInHierarchy)
        {
            subtitleTMP.transform.parent.gameObject.SetActive(false);
        }
    }
}
