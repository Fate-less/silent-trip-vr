using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEyeAnim : MonoBehaviour
{
    private Animator anim;
    public GameObject eyeObject;
    public List<GameObject> obj;
    public AudioSource audioSource;
    public AudioClip spottedSound;
    void Start()
    {
        anim = eyeObject.GetComponent<Animator>();
        StartCoroutine(delayGameover());
    }

    IEnumerator delayGameover()
    {
        yield return new WaitForSeconds(4f);
        eyeObject.SetActive(true);
        anim.Play("EyeOpenImage");
        audioSource.PlayOneShot(spottedSound);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);
        }
    }
}
