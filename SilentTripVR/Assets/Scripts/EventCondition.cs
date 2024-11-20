using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCondition : MonoBehaviour
{
    public List<StareDetection> npcList;
    public TimeManager timeManager;
    public EdgeCameraMovement cameraScript;
    public CameraZoom cameraZoom;

    public bool isAsking = false;

    public void WomanAsk()
    {
        if (timeManager.duration <= 60)
        {
            if (npcList[9] != null)
            {
                if (SceneManager.GetActiveScene().name == "PlaneGame")
                {
                    isAsking = true;
                    npcList[9].transform.GetChild(1).gameObject.SetActive(true);
                    npcList[9].cameraScript.LookAtObject(npcList[9].transform.GetChild(1).gameObject);
                    npcList[9].cameraScript.ZoomToObject(npcList[9].transform.GetChild(1).transform);
                }
                else
                {
                    isAsking = true;
                    npcList[9].transform.GetChild(2).gameObject.SetActive(true);
                    npcList[9].cameraScript.LookAtObject(npcList[9].gameObject);
                    npcList[9].cameraScript.ZoomToObject(npcList[9].transform);
                }
            }
        }
        else if (timeManager.duration <= 100)
        {
            if (npcList[0] != null)
            {
                if (SceneManager.GetActiveScene().name == "PlaneGame")
                {
                    isAsking = true;
                    npcList[4].transform.GetChild(1).gameObject.SetActive(true);
                    npcList[4].cameraScript.LookAtObject(npcList[4].transform.GetChild(2).gameObject);
                    npcList[4].cameraScript.ZoomToObject(npcList[4].transform.GetChild(2).transform);
                }
                else
                {
                    isAsking = true;
                    npcList[4].transform.GetChild(2).gameObject.SetActive(true);
                    npcList[4].cameraScript.LookAtObject(npcList[4].gameObject);
                    npcList[4].cameraScript.ZoomToObject(npcList[4].transform);
                }
            }
        }
        else if (timeManager.duration <= 245)
        {
            if (npcList[4] != null)
            {
                if (SceneManager.GetActiveScene().name == "PlaneGame")
                {
                    isAsking = true;
                    npcList[0].transform.GetChild(1).gameObject.SetActive(true);
                    npcList[0].cameraScript.LookAtObject(npcList[0].transform.GetChild(3).gameObject);
                    npcList[0].cameraScript.ZoomToObject(npcList[0].transform.GetChild(3).transform);
                }
                else
                {
                    isAsking = true;
                    npcList[0].transform.GetChild(2).gameObject.SetActive(true);
                    npcList[0].cameraScript.LookAtObject(npcList[0].gameObject);
                    npcList[0].cameraScript.ZoomToObject(npcList[0].transform);
                }
            }
        }
    }

    public void ManAsk()
    {
        if (npcList[2] != null)
        {
            if (SceneManager.GetActiveScene().name == "PlaneGame")
            {
                isAsking = true;
                npcList[2].transform.GetChild(1).gameObject.SetActive(true);
                npcList[2].cameraScript.LookAtObject(npcList[2].transform.GetChild(1).gameObject);
                npcList[2].cameraScript.ZoomToObject(npcList[2].transform);
            }
            else
            {
                isAsking = true;
                npcList[2].transform.GetChild(2).gameObject.SetActive(true);
                npcList[2].cameraScript.LookAtObject(npcList[2].gameObject);
                npcList[2].cameraScript.ZoomToObject(npcList[2].transform);
            }
        }
    }
}
