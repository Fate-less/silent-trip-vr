using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerSanity : MonoBehaviour
{
    public float anxiety = 100;
    public float multiplier = 1;
    public float decrementValue = 5;

    public CameraZoom cameraScript;

    private void Update()
    {
        if (anxiety > 100)
        {
            anxiety = 100;
        }
        if(anxiety < 0)
        {
            anxiety = 0;
        }
        if (!cameraScript.isAssured)
        {
            anxietyDecrease(decrementValue);
            decrementValue = decrementValue + Time.deltaTime;
        }
        else
        {
            if(SceneManager.GetActiveScene().name == "TrainGame")
            {
                anxietyDecrease(0.5f);
            }
            else
            {
                anxietyDecrease(0.4f);
            }
        }
    }

    public void anxietyDecrease(float decrement)
    {
        anxiety -= multiplier * decrement * Time.deltaTime;
    }

    public void anxietyIncrease(float increment)
    {
        anxiety += increment * Time.deltaTime;
    }
}
