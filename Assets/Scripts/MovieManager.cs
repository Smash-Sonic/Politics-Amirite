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
    public VideoClip ChigEntrance;
    public static bool VideoPlaying = false;
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        VideoPlaying = true;
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
        else if (MovieNumber == 5)
        {
            MoviePlayer.clip = ChigEntrance;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("space") || (int)MoviePlayer.time >= (int)MoviePlayer.length)
        {
            if (MovieNumber == 0)
            {
                VideoPlaying = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            else if (MovieNumber == 5)
            {
                for (int j = 0; j < GameController.HiredEmployees.Count; j++)
                {
                    GameController.HiredEmployees[j].SetActive(true);
                }
                VideoPlaying = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(4);
            }
            else if (MovieNumber == 1 || MovieNumber == 2 || MovieNumber == 3 || MovieNumber == 4)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
}
