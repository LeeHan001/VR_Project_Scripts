using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        PlayBGM();
    }
    public void PlayBGM()
    {
        if (bgmSource != null && bgmSource.clip != null)
        {
            bgmSource.loop = true; 
            bgmSource.Play();
            Debug.Log("music");
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

}