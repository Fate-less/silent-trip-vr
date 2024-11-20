using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    // Speed of the movement
    public float speed = 2.0f;
    // Distance to move from the original position
    public float distance = 100.0f; // UI units (typically pixels)

    // Original position of the RectTransform
    private Vector2 originalPosition;
    // Target position to move towards
    private Vector2 targetPosition;
    // Direction of movement
    private bool movingRight = true;
    // RectTransform component
    private RectTransform rectTransform;
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();
        // Store the original position of the RectTransform
        originalPosition = rectTransform.anchoredPosition;
        // Calculate the initial target position
        targetPosition = new Vector2(originalPosition.x + distance, originalPosition.y);
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameObject.CompareTag("Player"))
        {
            speed = 0f;
        }
        if (Input.GetMouseButtonUp(0) && gameObject.CompareTag("Player"))
        {
            speed = originalSpeed;
        }
        // Move the RectTransform towards the target position
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, speed * Time.deltaTime);
        // Check if the RectTransform has reached the target position
        if (rectTransform.anchoredPosition == targetPosition)
        {
            // Reverse the direction by toggling the movingRight flag
            movingRight = !movingRight;
            // Update the target position based on the direction
            if (movingRight)
            {
                targetPosition = new Vector2(originalPosition.x + distance, originalPosition.y);
            }
            else
            {
                targetPosition = new Vector2(originalPosition.x - distance, originalPosition.y);
            }
        }
    }
}
