using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieManager : MonoBehaviour
{
    public static int MovieNumber = 0;
    public VideoPlayer MoviePlayer;
    public VideoClip IntroMovie;
    public VideoClip Ending1;
    public VideoClip Ending2;
    public VideoClip Ending3;
    public VideoClip Ending4;
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (MovieNumber == 0)
        {
            MoviePlayer.clip = IntroMovie;
        }
        else if (MovieNumber == 1)
        {
            MoviePlayer.clip = Ending1;
        }
        else if (MovieNumber == 2)
        {
            MoviePlayer.clip = Ending2;
        }
        else if (MovieNumber == 3)
        {
            MoviePlayer.clip = Ending3;
        }
        else if (MovieNumber == 4)
        {
            MoviePlayer.clip = Ending4;
        }
    }
    void Update()
    {
        if ((int)MoviePlayer.time >= (int)MoviePlayer.length) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
