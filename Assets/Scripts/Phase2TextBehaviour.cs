using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase2TextBehaviour : MonoBehaviour
{
    public int RandomEvent;
    public string TextStore;
    public TMP_Text EventText;

    void Start()
    {
        EventText.text = null;
        RandomEvent = Random.Range(1,5);

        if (RandomEvent == 1)
        {
            TextStore = "Random event 1";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 2)
        {
            TextStore = "Random event 2";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 3)
        {
            TextStore = "Random event 3";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 4)
        {
            TextStore = "Random event 4";
            StartCoroutine(Scrolling());
        }
    }
    IEnumerator Scrolling()
    {
        foreach (char c in TextStore)
        {
            //adds the next character to the text every 0.05 seconds
            EventText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
