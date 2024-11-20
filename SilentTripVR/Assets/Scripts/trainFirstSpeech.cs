using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trainFirstSpeech : MonoBehaviour
{
    private subtitleScript subtitleScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(oneSecDelay());
    }

    IEnumerator oneSecDelay()
    {
        yield return new WaitForSeconds(1f);
        subtitleScript = GetComponent<subtitleScript>();
        subtitleScript.subtitleOn();
    }
}
