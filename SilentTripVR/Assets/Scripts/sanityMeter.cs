using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sanityMeter : MonoBehaviour
{
    public float sanity = 100;
    public playerSanity anxietyMeter;
    public Slider sliderObj;
    public TextMeshProUGUI hpTMP;
    public GameOverTransition gameOverTransition;

    // Update is called once per frame
    void Update()
    {
        if(anxietyMeter.anxiety <= 20)
        {
            sanity = sanity - Time.deltaTime;
        }
        sliderObj.value = sanity;
        hpTMP.text = sanity.ToString("0");
        if (sanity <= 0)
        {
            gameOverTransition.TriggerGameOver();
        }
    }
}
