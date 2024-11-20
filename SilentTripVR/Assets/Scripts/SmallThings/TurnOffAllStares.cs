using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAllStares : MonoBehaviour
{
    public EventCondition eventManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < eventManager.npcList.Count; i++)
        {
            eventManager.npcList[i].enabled = false;
        }
    }
}
