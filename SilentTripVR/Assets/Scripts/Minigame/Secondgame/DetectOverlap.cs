using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOverlap : MonoBehaviour
{
    public bool isOverlapping = false;
    public MinigameManager minigameManager;

    private void Start()
    {
        minigameManager = GameObject.Find("Minigame Manager").GetComponent<MinigameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOverlapping)
        {
            Debug.Log("Hit!");
            minigameManager.delayTransition.NextScreen();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOverlapping)
        {
            Debug.Log("Miss!");
            minigameManager.delayTransition.gadgetInteract.CloseGadget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Objective")
        {
            isOverlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Objective")
        {
            isOverlapping = false;
        }
    }
}
