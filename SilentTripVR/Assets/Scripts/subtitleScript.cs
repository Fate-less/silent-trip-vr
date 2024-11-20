using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class subtitleScript : MonoBehaviour
{
    public TextMeshProUGUI subtitleTMP;
    public string subtitleText;

    public void subtitleOn()
    {
        subtitleTMP.text = subtitleText;
        subtitleTMP.transform.parent.gameObject.SetActive(false);
        StartCoroutine(subtitleAppear());
    }
    public void subtitleOnPramugari()
    {
        subtitleTMP.text = subtitleText;
        subtitleTMP.transform.parent.gameObject.SetActive(false);
        StartCoroutine(subtitleAppear());
    }

    IEnumerator subtitleAppear()
    {
        subtitleTMP.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        if (subtitleTMP.transform.parent.gameObject.activeInHierarchy)
        {
            subtitleTMP.transform.parent.gameObject.SetActive(false);
        }
    }

    IEnumerator subtitleAppearPramugari()
    {
        subtitleTMP.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (subtitleTMP.transform.parent.gameObject.activeInHierarchy)
        {
            subtitleTMP.transform.parent.gameObject.SetActive(false);
        }
    }
}
