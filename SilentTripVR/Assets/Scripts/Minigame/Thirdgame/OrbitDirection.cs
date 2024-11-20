using UnityEngine;

public class OrbitDirection : MonoBehaviour
{
    public RectTransform centerObject;
    public float orbitSpeed = 5f;
    public float radius = 100f;

    private float angle = 0f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (centerObject == null)
            return;

        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        rectTransform.anchoredPosition = new Vector2(x, y) + centerObject.anchoredPosition;
    }
}
