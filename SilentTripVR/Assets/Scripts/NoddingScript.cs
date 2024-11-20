using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoddingScript : MonoBehaviour
{
    public float noddingSpeed = 1.0f;  // Speed of the nodding motion
    public float shakingSpeed = 1.0f;  // Speed of the shaking motion
    public float noddingAmount = 15.0f; // Amount of nodding (angle in degrees)
    public float shakingAmount = 15.0f; // Amount of shaking (angle in degrees)
    public EdgeCameraMovement playerFP;
    public bool isNodding = false;
    public bool isShaking = false;

    private float noddingTimer = 0.0f;
    private float shakingTimer = 0.0f;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        ApplyNodding();
        ApplyShaking();
    }

    public void HandleNodding()
    {
        isNodding = !isNodding;
        if (!isNodding)
        {
            noddingTimer = 0.0f;
            transform.localRotation = initialRotation;
            playerFP.enabled = false;
        }
    }

    public void HandleHeadshake()
    {
        isShaking = !isShaking;
        if (!isShaking)
        {
            shakingTimer = 0.0f;
            transform.localRotation = initialRotation;
            playerFP.enabled = false;
        }
    }

    void ApplyNodding()
    {
        if (isNodding)
        {
            noddingTimer += Time.deltaTime * noddingSpeed;
            float noddingAngle = Mathf.Sin(noddingTimer) * noddingAmount;
            Quaternion noddingRotation = Quaternion.Euler(noddingAngle, 0, 0);
            transform.localRotation = initialRotation * noddingRotation;
        }
    }

    void ApplyShaking()
    {
        if (isShaking)
        {
            shakingTimer += Time.deltaTime * shakingSpeed;
            float shakingAngle = Mathf.Sin(shakingTimer) * shakingAmount;
            Quaternion shakingRotation = Quaternion.Euler(0, shakingAngle, 0);
            transform.localRotation = initialRotation * shakingRotation;
        }
    }

    public void stopNodding()
    {
        StartCoroutine(delayNodding());
    }

    public void stopShaking()
    {
        StartCoroutine(delayShaking());
    }

    IEnumerator delayNodding()
    {
        yield return new WaitForSeconds(1.87f);
        isNodding = !isNodding;
        if (!isNodding)
        {
            noddingTimer = 0.0f;
            transform.localRotation = initialRotation;
        }
    }

    IEnumerator delayShaking()
    {
        yield return new WaitForSeconds(1.87f);
        isShaking = !isShaking;
        if (!isShaking)
        {
            shakingTimer = 0.0f;
            transform.localRotation = initialRotation;
        }
    }
}
