using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // Intensity of the shaking
    public float shakeSpeed = 1.0f; // Speed of the shaking
    public float maxRotationAngle = 5.0f; // Maximum angle the train can tilt

    private float timer = 0.0f;

    void Update()
    {
        // Simulate shaking by rotating the train slightly around its pivot
        float shakeAmount = Mathf.Sin(timer * shakeSpeed) * shakeIntensity;
        float rotationAngle = Random.Range(-shakeAmount, shakeAmount);
        rotationAngle = Mathf.Clamp(rotationAngle, -maxRotationAngle, maxRotationAngle);

        if (transform.rotation.x > 0.5 || transform.rotation.x < -0.5 ||
           transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
        {
            Debug.Log("exceeding maximum");
            transform.rotation = Quaternion.Euler(0,0,0);
            rotationAngle = 0;
            shakeAmount = 0;    
        }
        else
        {
            transform.Rotate(Vector3.forward, rotationAngle);
            transform.Rotate(Vector3.right, rotationAngle);
            transform.Rotate(0, 0, 0);
        }

        // Update timer
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            transform.Rotate(0, 0, 0);
            timer = 0;
            rotationAngle = 0;
            shakeAmount = 0;
        }
    }
}
