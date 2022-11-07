using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmployeeSelectBehaviour : MonoBehaviour
{
    public TMP_Text appliedtext;
    public List<GameObject> employees;
    public ButtonBehaviour buttonBehaviour;
    private bool employed = false;

    public void hired()
    {
        if (employed == false)
        {
            DontDestroyOnLoad(buttonBehaviour.employee);
            appliedtext.text = "Deselect";
            employed = true;
            Debug.Log("it will transfer");
        }

        else
        {
            appliedtext.text = "Hire";

            employed = false;
        }

    }
   
    // Update is called once per frame
    void Update()
    {
       
    }
}
