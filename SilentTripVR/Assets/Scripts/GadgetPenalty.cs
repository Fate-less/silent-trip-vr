using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetPenalty : MonoBehaviour
{
    [SerializeField] float limit;

    public GadgetInteract gadgetInteract;
    public float gadgetTimeCounter = 0f;
    public EventCondition eventCondition;

    // Update is called once per frame
    void Update()
    {
        if (gadgetInteract.isOpen)
        {
            gadgetTimeCounter++;
        }
        if(gadgetTimeCounter >= limit)
        {
            gadgetTimeCounter = 0;
            eventCondition.ManAsk();
        }

    }
}
