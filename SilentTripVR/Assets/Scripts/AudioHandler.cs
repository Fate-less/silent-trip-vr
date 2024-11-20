using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    public List<AudioClip> spotted;
    public List<AudioClip> sigh;
    public AudioClip entrance;
    public AudioClip breathe;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "TrainGame" || SceneManager.GetActiveScene().name == "PlaneGame")
        {
            StartCoroutine(delayOneSec());
        }
    }

    public void PlaySpottedSound()
    {
        int randomValue = Random.Range(0, 2);
        audioSource.PlayOneShot(spotted[randomValue]);
    }

    public void PlaySighSound()
    {
        int randomValue = Random.Range(0, 2);
        audioSource.PlayOneShot(sigh[randomValue]);
    }

    public void PlayEntranceSound()
    {
        audioSource.PlayOneShot(entrance);
    }

    public void PlayBreatheSound()
    {
        audioSource.PlayOneShot(breathe);
    }

    IEnumerator delayOneSec()
    {
        yield return new WaitForSeconds(1f);
        PlayEntranceSound();
    }
}
