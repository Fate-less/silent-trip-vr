using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCameraMovement : MonoBehaviour
{
    public float sensitivity = 40.0f; // Sensitivity of the camera movement
    public float edgeThreshold = 0.1f; // Threshold for edge detection (percentage of screen width)
    public float minRotationAngle = -30.0f; // Minimum rotation angle
    public float maxRotationAngle = 90.0f; // Maximum rotation angle
    public float currentRotationY;

    private float screenWidth;
    private float screenHeight;
    private Vector2 mousePosition;

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        currentRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;

        // Check if the mouse is at the left edge of the screen
        if (mousePosition.x <= screenWidth * edgeThreshold)
        {
            RotateCamera(-sensitivity);
        }
        // Check if the mouse is at the right edge of the screen
        else if (mousePosition.x >= screenWidth * (1.0f - edgeThreshold))
        {
            RotateCamera(sensitivity);
        }
    }

    void RotateCamera(float amount)
    {
        // Calculate the new rotation angle
        currentRotationY += amount * Time.deltaTime;
        // Clamp the rotation angle within the specified range
        currentRotationY = Mathf.Clamp(currentRotationY, minRotationAngle, maxRotationAngle);
        // Apply the clamped rotation to the camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotationY, transform.eulerAngles.z);
    }
}


