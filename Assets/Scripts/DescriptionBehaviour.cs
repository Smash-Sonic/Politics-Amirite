using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBehaviour : MonoBehaviour
{
    public GameObject TextBox;
    public GameObject Canvas;
    void OnMouseEnter()
    {
        TextBox.SetActive(true);
        Canvas.SetActive(true);
        //Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        TextBox.SetActive(false);
        Canvas.SetActive(false);
        //Debug.Log("Mouse is no longer on GameObject.");
    }
}
