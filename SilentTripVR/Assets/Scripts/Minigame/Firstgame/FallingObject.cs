using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    // Speed of the falling object
    public float distance = 200f;
    public float speed = 200f;
    public List<Sprite> spriteList;

    private RectTransform rectTransform;
    private Vector2 originalPos;
    private Vector2 targetPos;
    private int randomValue;

    private void Start()
    {
        randomValue = Random.Range(0, 7);
        rectTransform = gameObject.GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
        gameObject.GetComponent<Image>().sprite = spriteList[randomValue];
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object downward
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPos, speed * Time.deltaTime);
        targetPos = new Vector2(originalPos.x, originalPos.y - distance);

        // Destroy the object if it goes out of bounds (below the screen)
        if (rectTransform.anchoredPosition == targetPos)
        {
            Destroy(gameObject);
        }
    }
}
