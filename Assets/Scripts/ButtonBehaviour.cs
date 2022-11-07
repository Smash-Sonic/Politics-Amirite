using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonBehaviour : MonoBehaviour
{
    public int Strength;
    public int Intelligence;
    public int SocialSkills;
    public string Name;
    public TMP_Text StatText;
    public TMP_Text StaffName;
    public GameObject portrat;
    public GameObject employee;
    public List<GameObject> otherEmployee;
    private int i;
    public void OnButtonPress()
    {
        StaffName.text = "Applicant Name: " + Name;
        StatText.text = "Str = " + Strength + " Int = " + Intelligence + " Soc = " + SocialSkills;
        portrat.SetActive(true);
    }

    private void Update()
    {
        if(otherEmployee[i] == true)
        {
            portrat.SetActive(false);
        }
    }
}
