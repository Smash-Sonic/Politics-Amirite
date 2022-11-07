using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public int Strength;
    public int Intelligence;
    public int SocialSkills;
    public GameObject StatBox;
    public GameObject Canvas;
    public TMP_Text StatText;

    void Update()
    {
        StatText.text = "Str = " + Strength + " Int = " + Intelligence + " Soc = " + SocialSkills;
    }

    void OnMouseEnter()
    {
        StatBox.SetActive(true);
        Canvas.SetActive(true);
        StatText.gameObject.SetActive(true);
       // Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        StatBox.SetActive(false);
        Canvas.SetActive(false);
        StatText.gameObject.SetActive(false);
       // Debug.Log("Mouse is no longer on GameObject.");
    }
}
