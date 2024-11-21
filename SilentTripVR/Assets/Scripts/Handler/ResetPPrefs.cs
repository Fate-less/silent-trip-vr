using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
