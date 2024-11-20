using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirectionDetector : MonoBehaviour
{
    // Variables to set the time threshold
    public float timeThreshold = 2.0f; // Time in seconds the camera needs to be facing up or down
    private float timeFacingUp = 0f;
    private float timeFacingDown = 0f;

    // Update is called once per frame
    void Update()
    {
        // Get the forward direction of the camera
        Vector3 forward = transform.forward;

        // Check if the camera is facing up
        if (Vector3.Dot(forward, Vector3.up) > 0.5f)
        {
            timeFacingUp += Time.deltaTime;
            timeFacingDown = 0f; // Reset the down timer
            if (timeFacingUp >= timeThreshold)
            {
                OnCameraFacingUp();
                timeFacingUp = 0f; // Reset the up timer after the method call
            }
        }
        // Check if the camera is facing down
        else if (Vector3.Dot(forward, Vector3.down) > 0.5f)
        {
            timeFacingDown += Time.deltaTime;
            timeFacingUp = 0f; // Reset the up timer
            if (timeFacingDown >= timeThreshold)
            {
                OnCameraFacingDown();
                timeFacingDown = 0f; // Reset the down timer after the method call
            }
        }
        else
        {
            // Reset both timers if the camera is not facing up or down
            timeFacingUp = 0f;
            timeFacingDown = 0f;
        }
    }

    // Method to be called when the camera is facing up for too long
    void OnCameraFacingUp()
    {
        Debug.Log("Camera has been facing up for too long!");
        // Your custom logic here
    }

    // Method to be called when the camera is facing down for too long
    void OnCameraFacingDown()
    {
        Debug.Log("Camera has been facing down for too long!");
        // Your custom logic here
    }
}

