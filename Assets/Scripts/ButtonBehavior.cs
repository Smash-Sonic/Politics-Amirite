using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorialButton1;
    public GameObject tutorialButton2;
    public GameObject tutorialButton3;

    public void SceneChange(int sceneID)//sets up scene changing
    {
        SceneManager.LoadScene(sceneID);
    }

    public void Quit()//quits the game
    {
        Application.Quit();
    }

    public void tut1() //progresses the first part of the tutorial
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        tutorialButton1.SetActive(false);
        tutorialButton2.SetActive(true);
    }

    public void tut2() //progresses the second part of the tutorial
    {
        tutorial2.SetActive(false);
        tutorial3.SetActive(true);
        tutorialButton2.SetActive(false);
        tutorialButton3.SetActive(true);
    }
}
