using UnityEngine;

public class HeartbeatVFX : MonoBehaviour
{
    public Camera cameraToAnimate; // Assign the camera in the inspector
    public float zoomInFOV = 60f; // The FOV value for zoom-in
    public float zoomOutFOV = 70f; // The FOV value for zoom-out
    public float heartbeatDuration = 1f; // Total duration for two beats

    private float originalFOV;
    private bool isAnimating = false;

    void Start()
    {
        if (cameraToAnimate == null)
        {
            cameraToAnimate = Camera.main;
        }

        originalFOV = cameraToAnimate.fieldOfView;
    }

    public void TriggerHeartbeat()
    {
        if (!isAnimating)
        {
            StartCoroutine(HeartbeatRoutine());
        }
    }

    private System.Collections.IEnumerator HeartbeatRoutine()
    {
        isAnimating = true;

        float halfDuration = heartbeatDuration / 4f;
        float time = 0f;

        // First zoom in
        while (time < halfDuration)
        {
            cameraToAnimate.fieldOfView = Mathf.Lerp(originalFOV, zoomInFOV, time / halfDuration);
            time += Time.deltaTime;
            yield return null;
        }
        cameraToAnimate.fieldOfView = zoomInFOV;

        // First zoom out
        time = 0f;
        while (time < halfDuration)
        {
            cameraToAnimate.fieldOfView = Mathf.Lerp(zoomInFOV, zoomOutFOV, time / halfDuration);
            time += Time.deltaTime;
            yield return null;
        }
        cameraToAnimate.fieldOfView = zoomOutFOV;

        // Second zoom in
        time = 0f;
        while (time < halfDuration)
        {
            cameraToAnimate.fieldOfView = Mathf.Lerp(zoomOutFOV, zoomInFOV, time / halfDuration);
            time += Time.deltaTime;
            yield return null;
        }
        cameraToAnimate.fieldOfView = zoomInFOV;

        // Second zoom out back to original FOV
        time = 0f;
        while (time < halfDuration)
        {
            cameraToAnimate.fieldOfView = Mathf.Lerp(zoomInFOV, originalFOV, time / halfDuration);
            time += Time.deltaTime;
            yield return null;
        }
        cameraToAnimate.fieldOfView = originalFOV;

        isAnimating = false;
    }
}
