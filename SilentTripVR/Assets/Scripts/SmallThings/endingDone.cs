using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingDone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayToMainmenu());
    }

    IEnumerator delayToMainmenu()
    {
        yield return new WaitForSeconds(97f);
        SceneManager.LoadScene("Mainmenu");
    }
}
