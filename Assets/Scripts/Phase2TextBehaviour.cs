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
            TextStore = "A child is lost in the store and can’t find their parents, the child is extremely nervous and is on the brink of crying and screaming, send someone friendly to go with the child to find their parents.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 2)
        {
            TextStore = "An old Woman tells customer service that she has lost her purse, send someone to help her find it.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 3)
        {
            TextStore = "The Cleanup Company™ accepts a brand deal with your store, one of your employees needs to dress up as the company mascot: Captain Cleanup™ and stand outside the store promoting the new line of Cleanup detergent™";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 4)
        {
            TextStore = "There has been a spill of bleach in aisle 6. Send an employee to clean up the mess.";
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
