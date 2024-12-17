using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public List<GameObject> npcFirstCycle;
    public List<GameObject> npcSecondCycle;
    public List<GameObject> npcThirdCycle;

    private void Start()
    {
        for (int i = 0; i < npcSecondCycle.Count; i++)
        {
            SetActiveNPCSecondCycle(false);
            SetActiveNPCThirdCycle(false);
            npcSecondCycleFadeIn(0, 0.2f);
            npcThirdCycleFadeIn(0, 0.2f);
        }
    }

    private void npcSecondCycleFadeIn(float endValue, float duration)
    {
        npcSecondCycle[i].GetComponent<FadeInNPC>().SpriteFade(npcSecondCycle[i].GetComponent<SpriteRenderer>(), endValue, duration);
    }
    private void npcThirdCycleFadeIn(float endValue, float duration)
    {
        npcThirdCycle[i].GetComponent<FadeInNPC>().SpriteFade(npcThirdCycle[i].GetComponent<SpriteRenderer>(), endValue, duration);
    }
    public void SetActiveNPCSecondCycle(bool isActive)
    {
        for (int i = 0; i < npcSecondCycle.Count; i++)
        {
            npcSecondCycle[i].SetActive(isActive);
        }
    }
    public void SetActiveNPCThirdCycle(bool isActive)
    {
        for (int i = 0; i < npcThirdCycle.Count; i++)
        {
            npcThirdCycle[i].SetActive(isActive);
        }
    }
}
