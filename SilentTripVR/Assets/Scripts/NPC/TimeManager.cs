using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private NPCHandler npcHandler;
    private void Start()
    {
        npcHandler = GameObject.Find("Passengers").GetComponent<NPCHandler>();
        StartCoroutine(NextCycle());
    }

    IEnumerator NextCycle()
    {
        yield return new WaitForSeconds(120);
        npcHandler.NextCycle();
    }
}
