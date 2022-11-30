using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip DailyLife;
    public AudioClip DeadlyLife;
    public static bool MusicPlaying;

    void Start()
    {
        if (MusicPlaying == false)
        {
            DontDestroyOnLoad(this.gameObject);
            audioSource.clip = DailyLife;
            MusicPlaying = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        /*
        if (GameController.CurrentDay == 7)
        {
            audioSource.clip = DeadlyLife;
        }
        */

        if (MovieManager.VideoPlaying == true)
        {
            audioSource.Stop();
            if (MovieManager.MovieNumber == 5)
            {
                audioSource.clip = DeadlyLife;
            }
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
