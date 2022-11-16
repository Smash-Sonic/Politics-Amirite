using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip DailyLife;
    public AudioClip DeadlyLife;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource.clip = DailyLife;
    }
    void Update()
    {
        if (GameController.CurrentDay == 7)
        {
            audioSource.clip = DeadlyLife;
        }

        if (MovieManager.VideoPlaying == true)
        {
            audioSource.Stop();
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
