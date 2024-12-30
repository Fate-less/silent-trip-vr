using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private NPCHandler npcHandler;
    public GameEndTransition gameEndTransition;
    private Coroutine cycleCoroutine;
    private void Start()
    {
        npcHandler = GameObject.Find("Passengers").GetComponent<NPCHandler>();
    }
    private void Update()
    {
        if(cycleCoroutine == null)
        {
            cycleCoroutine = StartCoroutine(NextCycle());
        }
    }

    IEnumerator NextCycle()
    {
        yield return new WaitForSeconds(120);
        npcHandler.NextCycle();
        cycleCoroutine = null;
    }
}
