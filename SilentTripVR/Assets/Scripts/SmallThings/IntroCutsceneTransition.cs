using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayCutsceneTransition());
    }

    IEnumerator delayCutsceneTransition()
    {

        yield return new WaitForSeconds(45f);
        SceneManager.LoadScene("Mainmenu");
    }
}
