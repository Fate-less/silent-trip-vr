using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public bool isAssured = true;
    public float zoomDuration = 0.5f; // Duration of the zoom effect
    public float maxFOV = 40f; // Maximum FOV for the zoom-in effect
    public float minFOV = 60f; // Minimum FOV for the zoom-out effect
    public GameObject lastStarePerson;
    public EdgeCameraMovement playerFP;
    public GameObject playerObj;

    private Camera cam;
    private float originalFOV;
    private bool isZooming = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalFOV = cam.fieldOfView;
    }

    public void ZoomToObject(Transform target)
    {
        if (!isZooming)
        {
            StartCoroutine(ZoomCoroutine(target));
        }
    }

    private IEnumerator ZoomCoroutine(Transform target)
    {
        isZooming = true;
        playerFP.enabled = false;

        // Zoom in
        float startTime = Time.time;
        while (Time.time - startTime < zoomDuration)
        {
            float t = (Time.time - startTime) / zoomDuration;
            cam.fieldOfView = Mathf.Lerp(originalFOV, maxFOV, t);
            yield return null;
        }

        // Zoom out slightly
        startTime = Time.time;
        while (Time.time - startTime < zoomDuration / 2f)
        {
            float t = (Time.time - startTime) / (zoomDuration  / 2f);
            cam.fieldOfView = Mathf.Lerp(maxFOV, minFOV, t);
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < zoomDuration)
        {
            yield return null; 
        }
        cam.fieldOfView = originalFOV;
        playerFP.enabled = true;
        //cam.transform.rotation = originalPOS.rotation;
        isZooming = false;
    }

    public void LookAtObject(GameObject objectToLook)
    {
        playerObj.transform.LookAt(new Vector3(objectToLook.transform.position.x,playerObj.transform.position.y, objectToLook.transform.position.z));
    }

    public IEnumerator RotateBack()
    {
        yield return new WaitForSeconds(1.25f);
        playerObj.transform.rotation = Quaternion.Euler(new Vector3(playerObj.transform.rotation.x, playerFP.currentRotationY, playerObj.transform.rotation.z));
    }
}

