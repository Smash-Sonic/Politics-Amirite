using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public List<GameObject> CharacterPortraits;
    public List<GameObject> Employees;
    public List<bool> Hired;
    public TMP_Text appliedtext;
    public TMP_Text numberedEmployeeText;
    public int CharacterSelected;
    public static int HowManyEmployees = 0;
    public static int EmployeesLeft = 5;
    public AudioClip Click;
    public AudioClip Hire;
    public Image image;

    public void ToggleCharacter(int CharacterClicked)
    {
        Vector3 camPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(Click, camPos);
        CharacterSelected = CharacterClicked;
        for (int i = 0; i < CharacterPortraits.Count; i++)
        {
            if (i == CharacterClicked)
            {
                CharacterPortraits[i].SetActive(true);
            }
            else
            {
                CharacterPortraits[i].SetActive(false);
            }
        }
        if (Hired[CharacterSelected] == false)
        {
            appliedtext.text = "Hire";
        }
        else
        {
            appliedtext.text = "Already Hired";
        }
    }

    public void HireCharacter()
    {
        if (Hired[CharacterSelected] == false && HowManyEmployees < 5)
        {
            AudioSource.PlayClipAtPoint(Hire, new Vector3(0, 0, -8));
            Hired[CharacterSelected] = true;
            DontDestroyOnLoad(Employees[CharacterSelected]);
            GameController.HiredEmployees.Add(Employees[CharacterSelected]);
            appliedtext.text = "Already Hired";
            HowManyEmployees++;
            EmployeesLeft--;
            if (EmployeesLeft == 0)
            {
                numberedEmployeeText.text = "Click Here to Continue to the Next Phase";
            }
            else
            {
                numberedEmployeeText.text = "Next Phase (Requires " + EmployeesLeft + " Employees)";
            }
        }
    }
}
