using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverTransition : MonoBehaviour
{
    public float blinkDuration = 2.0f; // Duration of the entire blink effect
    public float blinkSpeed = 0.5f; // Speed of the blinking
    public Image fadeImage; // UI Image for fading (should cover the entire screen)

    private float timer = 0.0f;
    private bool isGameOver = false;
    private Color originalColor;

    void Start()
    {
        // Ensure the fadeImage is fully transparent at the start
        originalColor = fadeImage.color;
        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    void Update()
    {
        if (isGameOver)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(timer * Mathf.PI / blinkSpeed));
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (timer >= blinkDuration)
            {
                // End of the transition, you can load the game over screen or any other action
                // For example: SceneManager.LoadScene("GameOverScene");
                SceneManager.LoadScene("Gameover");
            }
        }
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            fadeImage.gameObject.SetActive(true);
            isGameOver = true;
            timer = 0.0f;
        }
    }
}
