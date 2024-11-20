using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationLimiter : MonoBehaviour
{
    // Sensitivity for the camera rotation
    public float sensitivity = 5.0f;

    // Variables to store the current rotation
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Adjust rotation based on mouse input
        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp the rotationX to -90 to 90 degrees to prevent flipping
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Limit the rotationY to 0 to 90 degrees for right rotation
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
    }
}
