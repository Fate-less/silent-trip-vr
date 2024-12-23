using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyMeter : MonoBehaviour
{
    public float anxiety = 0f;

    private void Update()
    {
        anxiety += 0.005f * Time.deltaTime;
    }
}
