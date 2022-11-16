using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int Money = 0; //in 100s
    public static int Relationship = 0;
    public static int CurrentDay = 1;
    public TMP_Text DayText;
    public TMP_Text RelationshipText;
    //public static GameObject[] HiredEmployees;
    public static List<GameObject> HiredEmployees = new List<GameObject>();

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            /*
            Debug.Log(HiredEmployees.Count);
            for (int i = 0; i < HiredEmployees.Count; i++)
            {
                Debug.Log(HiredEmployees[i]);
            }
            */
        }
        //don't have day text when picking employees at the start of the game
        if (SceneManager.GetActiveScene().name != "Phase 0")
        {
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
        if (SceneManager.GetActiveScene().name == "Phase 3") 
        {
            RelationshipText.text = "Wharmongra Relationship: " + Relationship;
        }
    }
}
