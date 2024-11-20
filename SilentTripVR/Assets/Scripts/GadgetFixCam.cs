using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetFixCam : MonoBehaviour
{
    public GameObject StartPos;
    public GameObject playerObj;
    public float zoomDuration;

    public void FixCameraOpen()
    {
        LeanTween.rotate(playerObj, new Vector3(0, 0, 0), 1.6f).setEaseOutQuart();
        LeanTween.rotate(playerObj, new Vector3(20, 0, 0), 1f).setEaseOutQuart().setDelay(1.5f);
    }

    public void FixCameraClose()
    {
        LeanTween.rotate(playerObj, new Vector3(0, 0, 0), 1f).setEaseOutQuart();
    }

    public void PlaneFixCameraOpen()
    {
        LeanTween.rotate(playerObj, new Vector3(0, 0, 0), 1.6f).setEaseOutQuart();
        LeanTween.rotate(playerObj, new Vector3(8.5f, 0, 0), 1f).setEaseOutQuart().setDelay(1.5f);
        StartCoroutine(delayZoomIn());
    }

    public void PlaneFixCameraClose()
    {
        StartCoroutine(ZoomOut());
    }

    IEnumerator delayZoomIn()
    {
        yield return new WaitForSeconds(3);
        Camera.main.fieldOfView = 30;
        float startTime = Time.time;
        while (Time.time - startTime < zoomDuration)
        {
            float t = (Time.time - startTime) / zoomDuration;
            Camera.main.fieldOfView = Mathf.Lerp(60, 30, t);
            yield return null;
        }
    }

    IEnumerator ZoomOut()
    {
        float startTime = Time.time;
        Camera.main.fieldOfView = 60;
        while (Time.time - startTime < zoomDuration / 2f)
        {
            float t = (Time.time - startTime) / (zoomDuration / 2f);
            Camera.main.fieldOfView = Mathf.Lerp(30, 60, t);
            yield return null;
        }
        LeanTween.rotate(playerObj, new Vector3(0, 0, 0), 1f).setEaseOutQuart();
    }
}
