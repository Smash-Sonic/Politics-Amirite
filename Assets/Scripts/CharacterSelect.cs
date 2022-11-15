using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public List<GameObject> CharacterPortraits;
    public List<GameObject> Employees;
    public List<bool> Hired;
    public TMP_Text appliedtext;
    public int CharacterSelected;
    public static int HowManyEmployees = 0;

    public void ToggleCharacter(int CharacterClicked)
    {
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
            Hired[CharacterSelected] = true;
            DontDestroyOnLoad(Employees[CharacterSelected]);
            GameController.HiredEmployees.Add(Employees[CharacterSelected]);
            appliedtext.text = "Already Hired";
            HowManyEmployees++;
        }
    }
}
