using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StareDetection : MonoBehaviour
{
    public float stareDurationThreshold = 3.0f; // Duration threshold for triggering the event
    public GameObject playerCamera; // Reference to the player's camera
    public bool isStaring = false; // Flag to track if the player is staring at the object
    public GameObject eyeObject;


    private float stareTimer = 0.0f;

    void Update()
    {
        // Check if the player is looking at the object
        Vector3 playerToTarget = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, playerToTarget);
        if (angle < 30f) // Adjust this angle to define the threshold for "staring"
        {
            // Increment the stare timer if the player is looking at the object
            stareTimer += Time.deltaTime;
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
    }

    public void eyeStare()
    {
        eyeObject.SetActive(true);
        eyeObject.GetComponent<Animator>().SetBool("isStaring", true);
        eyeObject.GetComponent<Animator>().Play("EyeOpen");
        isStaring = true;
    }

    public IEnumerator eyeClose()
    {
        yield return new WaitForSeconds(2f);
        eyeObject.SetActive(false);
    }
}
