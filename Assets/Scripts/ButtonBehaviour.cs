using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBehaviour : MonoBehaviour
{
    public int Strength;
    public int Intelligence;
    public int SocialSkills;
    public string Name;
    public string Bio;
    public TMP_Text StatText;
    public TMP_Text StaffName;
    public GameObject portrait;
    public GameObject employee;
    public Image image;
    public void OnButtonPress()
    {
        StaffName.text = "Name: " + Name;

        StatText.text = "Str = " + Strength + " Int = " + Intelligence + " Soc = " + SocialSkills + "                                              " + Bio;
    }

    public void BackgroundDarken()
    {
        image.GetComponent<Image>().color = new Color(100, 0, 0, 255);
    }
}
