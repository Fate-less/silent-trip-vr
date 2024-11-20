using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public MinigameManager minigameManager;
    public RectTransform orbitingObject;
    public float pushForce = 500f;
    private RectTransform rectTransform;
    private bool isPushed = false;
    private Vector2 pushDirection;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        minigameManager = GameObject.Find("Minigame Manager").GetComponent<MinigameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushInCurrentDirection();
        }
        if (isPushed)
        {
            rectTransform.anchoredPosition += pushDirection * pushForce * Time.deltaTime;
        }
    }

    void PushInCurrentDirection()
    {
        if (orbitingObject == null)
        {
            Debug.LogError("Orbiting object is not set!");
            return;
        }

        // Calculate the direction from the center object to the orbiting object
        pushDirection = (orbitingObject.anchoredPosition - rectTransform.anchoredPosition).normalized;
        isPushed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            minigameManager.delayTransition.gadgetInteract.CloseGadget();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Goal")
        {
            minigameManager.delayTransition.NextScreen();
        }
    }
}
