using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneCutsceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayCutsceneTransition());
    }

    IEnumerator delayCutsceneTransition()
    {
        yield return new WaitForSeconds(59f);
        SceneManager.LoadScene("tutorialPesawat");
    }
}
