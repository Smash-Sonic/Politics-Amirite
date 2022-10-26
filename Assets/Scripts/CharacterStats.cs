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
    public TMP_Text StatText;

    void Update()
    {
        StatText.text = "Str = " + Strength + " Int = " + Intelligence + " Soc = " + SocialSkills;
    }

    void OnMouseEnter()
    {

        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
