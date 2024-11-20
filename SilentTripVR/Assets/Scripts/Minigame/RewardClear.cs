using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardClear : MonoBehaviour
{
    public MinigameManager minigameManager;

    private void Start()
    {
        minigameManager = GameObject.Find("Minigame Manager").GetComponent<MinigameManager>();
        StartCoroutine(delayNext());
    }

    IEnumerator delayNext()
    {
        yield return new WaitForSeconds(2f);
        minigameManager.delayTransition.NextScreen();
        minigameManager.playerSanity.anxiety = minigameManager.playerSanity.anxiety + 20;
    }
}
