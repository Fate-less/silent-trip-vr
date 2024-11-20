using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatboxDuration : MonoBehaviour
{
    private float timer;
    public float duration = 10f;
    public sanityMeter sanityMeter;
    public EventCondition eventManager;

    private AudioSource audioSource;

    private void Start()
    {
        timer = duration;
        audioSource = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(audioSource.clip, this.gameObject.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if(timer < 0)
        {
            gameObject.SetActive(false);
            sanityMeter.sanity = sanityMeter.sanity - 20;
            eventManager.isAsking = false; 
        }
    }
}
