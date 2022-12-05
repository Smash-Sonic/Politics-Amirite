using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int Money; //in 100s
    public static int Relationship = 0;
    public static int CurrentDay = 1;
    public static bool Paused;
    public TMP_Text DayText;
    public TMP_Text RelationshipText;
    public GameObject ButtonResume;
    public GameObject ButtonMainMenu;
    public GameObject PauseBackground;
    public GameObject Curser;
    public Camera cam;
    public static List<GameObject> HiredEmployees = new List<GameObject>();
    private void Start()
    {
        ButtonResume.SetActive(false);
        ButtonMainMenu.SetActive(false);
        Paused = false;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Paused = true;
            PauseBackground.SetActive(true);
            ButtonResume.SetActive(true);
            ButtonMainMenu.SetActive(true);
        }
        if(ButtonResume.activeSelf)
        {
            Curser.transform.position = new Vector3(20, 20, 0);
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
    public void ToggleUnpause()
    {
        Paused = false;
        Curser.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        ButtonResume.SetActive(false);
        ButtonMainMenu.SetActive(false);
        PauseBackground.SetActive(false);
    }
    public void ToMainMenu()
    {
        for (int j = 0; j < HiredEmployees.Count; j++)
        {
            //no more bozos hanging out where they shouldn't be
            Destroy(GameController.HiredEmployees[j]);
        }
        HiredEmployees = new List<GameObject>();
        Paused = false;
        MovieManager.MovieNumber = 0;
        CurrentDay = 1;
        SceneManager.LoadScene(0);
    }
}
