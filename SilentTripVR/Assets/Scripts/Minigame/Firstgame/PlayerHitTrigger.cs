using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    public MinigameManager minigameManager;

    private void Start()
    {
        minigameManager = GameObject.Find("Minigame Manager").GetComponent<MinigameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            minigameManager.delayTransition.gadgetInteract.CloseGadget();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
