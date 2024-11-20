using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMinigameTutorial : MonoBehaviour
{
    public void DeleteTutorialPPrefs()
    {
        PlayerPrefs.DeleteKey("Minigame Tutorial");
    }
}
