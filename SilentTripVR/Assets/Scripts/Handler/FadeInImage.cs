using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
    public IEnumerator ImageFade(Image img, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = img.color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            yield return null;
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, endValue);
    }
}
