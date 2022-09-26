using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int CurrentDay = 1;
    public TMP_Text DayText;

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        switch (CurrentDay)
        {
            case 1:
                DayText.text = "Monday:    Day " + CurrentDay;
                break;
            case 2:
                DayText.text = "Tuesday:   Day " + CurrentDay;
                break;
            case 3:
                DayText.text = "Wednesday: Day " + CurrentDay;
                break;
            case 4:
                DayText.text = "Thursday:  Day " + CurrentDay;
                break;
            case 5:
                DayText.text = "Friday:    Day " + CurrentDay;
                break;
            case 6:
                DayText.text = "Saturday:  Day " + CurrentDay;
                break;
            case 7:
                DayText.text = "Sunday:    Day " + CurrentDay;
                break;
            default:
                DayText.text = "???";
                break;
        }
    }
}
