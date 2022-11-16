/*****************************************************************************
// File Name :         ButtonBehavior.cs
// Author :            Alex Laubenstein
// Creation Date :     November 1, 2022
//
// Brief Description : This is a Script that sets up the buttons for use in the
                       main menu
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonBehavior : MonoBehaviour
{
    //Sets up game objects for later use
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorialButton1;
    public GameObject tutorialButton2;
    public GameObject tutorialButton3;
    public TMP_Text PhaseText;

    public void Start()
    {
        //PhaseText.text = "For Phase 1:";
    }
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
        PhaseText.text = "For Phase 2:";
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        tutorialButton1.SetActive(false);
        tutorialButton2.SetActive(true);
    }

    public void tut2() //progresses the second part of the tutorial
    {
        PhaseText.text = "For Phase 3:";
        tutorial2.SetActive(false);
        tutorial3.SetActive(true);
        tutorialButton2.SetActive(false);
        tutorialButton3.SetActive(true);
    }
}
