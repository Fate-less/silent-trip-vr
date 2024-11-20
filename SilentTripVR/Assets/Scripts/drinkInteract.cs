using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drinkInteract : MonoBehaviour
{
    public playerSanity anxietyMeter;
    public GameObject bottleObj;
    public float waterDrank;
    public Image waterUseOnce;
    public Image waterUseTwice;

    private bool isDrinking = false;

    private void Update()
    {
        if(waterDrank == 0)
        {
            waterUseOnce.color = Color.white;
            waterUseTwice.color = Color.white;
        }
        if (waterDrank >= 1)
        {
            waterUseOnce.color = Color.clear;
            waterUseTwice.color = Color.white;
        }
        if (waterDrank >= 2)
        {
            waterUseTwice.color = Color.clear;
        }
    }

    public void DrinkWater()
    {
        if(waterDrank < 2 && !isDrinking)
        {
            isDrinking = true;
            anxietyMeter.anxiety = anxietyMeter.anxiety + 40;
            bottleObj.SetActive(true);
            StartCoroutine(StopDrinking());
            waterDrank++;
        }
    }

    IEnumerator StopDrinking()
    {
        yield return new WaitForSeconds(1.3f);
        bottleObj.SetActive(false);
        isDrinking = false;
    }

}
