using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playbackVolume : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        videoPlayer.SetDirectAudioVolume(0, volume);
    }
}
