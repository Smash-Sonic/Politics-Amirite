using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase2TextBehaviour : MonoBehaviour
{
    public static bool ButtonPressed;
    public static bool TextFinished = false;
    public int RandomEvent;
    public string TextStore;
    public TMP_Text EventText;
    public static int LastEvent = 99;
    public static int CharacterStr;
    public static int CharacterInt;
    public static int CharacterSoc;
    public static string CharacterName;

    void Start()
    {
        TextFinished = false;
        EventText.text = null;
        RandomEvent = Random.Range(1,9);
        if (RandomEvent == LastEvent)
        {
            //reroll
            RandomEvent = Random.Range(1, 9);
        }
        if (RandomEvent == 1)
        {
            TextStore = null;
            TextStore = "A child is lost in the store and can�t find their parents, the child is extremely nervous and is on the brink of crying and screaming, send someone friendly to go with the child to find their parents.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 2)
        {
            TextStore = null;
            TextStore = "An old Woman tells customer service that she has lost her purse, send someone to help her find it.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 3)
        {
            TextStore = null;
            TextStore = "The Cleanup Company� accepts a brand deal with your store. One of your employees needs to dress up as the company mascot: Captain Cleanup� and stand outside the store to promote the new line of Cleanup detergent�.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 4)
        {
            TextStore = null;
            TextStore = "There has been a spill of bleach in aisle 6. Send an employee to clean up the mess.";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 5)
        {
            TextStore = null;
            TextStore = "A representative of Wharmongra arrives at the store to show off the superiority of their country, the diplomat challenges you to an arm wrestling contest, winning the contest would only provoke Wharmongra";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 6)
        {
            TextStore = null;
            TextStore = "A lawyer and his son are trapped in the elevator, send someone to help get them out of the elevator";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 7)
        {
            TextStore = null;
            TextStore = "A con artist has entered the store, claiming that someone in the store owes them money, send someone to deal with them without being talkative";
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 8)
        {
            TextStore = null;
            TextStore = "Someone drops a quarter into a manhole, there is a green sludge at the bottom, the quarter is not worth it, only an idiot would go after it� But maybe an idiot is exactly what you need!";
            StartCoroutine(Scrolling());
        }
    }
    void Update()
    {
        if (ButtonPressed == true)
        {
            TextStore = null;
            EventText.text = null;
            ButtonPressed = false;
            StopCoroutine(Scrolling());
            ResultText();
        }
    }
    void ResultText()
    {
        //Debug.Log("Character had " + CharacterStr + " strength, " + CharacterInt + " intelligence, " + CharacterSoc + " social skills.");
        if (RandomEvent == 1)
        {
            if (CharacterSoc >= 4)
            {
                TextStore = null;
                TextStore = CharacterName + " is kind and welcoming to the child and calms them down, after a little searching the parents of the child are found and the family is happily reunited. +1 Relationship.";
                GameController.Relationship++;
            }
            else
            {
                TextStore = null;
                TextStore = CharacterName + " approaches the child but before they can get to them, the child screams and runs away. This causes a scene and the police are called to the store. -$100 in lost income.";
                GameController.Money--;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 2)
        {
            if (CharacterSoc >= 3)
            {
                TextStore = null;
                TextStore = CharacterName + " is able to locate the missing purse and is kind to the elderly woman, [Character name] is given a cookie as a reward from the elderly woman. +1 Relationship.";
                GameController.Relationship++;
            }
            else if (CharacterSoc < 3)
            {
                TextStore = null;
                TextStore = CharacterName + " searches the entire store but cannot find the missing purse, [Character name] then has the �brilliant idea� to search the Wharmongran embassy, [Character name] is later arrested for trespassing. -$100 in lost income and -1 relationship.";
                GameController.Money--;
                GameController.Relationship++;
            }
            //possibly add in a third ending where the character simply doesn't find the purse and nothing happens if they don't have low int?
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 3)
        {
            if (CharacterStr >= 3)
            {
                TextStore = CharacterName + " as Captain Cleanup� is a huge hit with customers! The Cleanup Company� gives you a bonus payment of $200.";
                GameController.Money = GameController.Money + 2;
            }
            else if (CharacterStr < 3)
            {
                TextStore = CharacterName + " as Captain Cleanup� is a menace to society and scares away many potential customers. You lose $100 of your average earnings today.";
                GameController.Money--;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 4)
        {
            if (CharacterInt >= 3)
            {
                TextStore = CharacterName + " successfully cleans up the spill and there are no casualties. +1 Relationship.";
                GameController.Relationship++;
            }
            else if (CharacterInt < 3)
            {
                TextStore = CharacterName + " Fails so miserably that the fire department is called to the store. -$100 in lost income";
                GameController.Money--;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 5)
        {
            if (CharacterStr <= 3)
            {
                TextStore = CharacterName + "cannot beat the diplomat in a basic test of strength with their goofy little noodle arms. Wharmongra reaffirms their superiority to themselves +2 relationship.";
                GameController.Relationship = GameController.Relationship + 2;
            }
            else if (CharacterStr > 3)
            {
                TextStore = CharacterName + "wipes the floor with the diplomat, Wharmongra takes this as an act of aggression against their wonderful nation! -3 relationship.";
                GameController.Relationship = GameController.Relationship - 3;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 6)
        {
            if (CharacterInt >= 9)
            {
                TextStore = CharacterName + " Successfully gets them out of the elevator, the lawyer gives you a big tip.";
                GameController.Money++;
            }
            else if (CharacterInt < 9)
            {
                TextStore = CharacterName + "is unable to get the two people out of the elevator, 5 hours later they are finally free. The pair both suffer brain damage. The lawyer is enraged and sues the store.";
                GameController.Money--;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 7)
        {
            if (CharacterSoc <= 4)
            {
                TextStore = CharacterName + " kicks out the con artist without speaking a single word, Wharmongra likes the no nonsense approach +1 to relationship";
                GameController.Relationship++;
            }
            else
            {
                TextStore = CharacterName + "  approaches the strange man and says something kind to the man, before they can blink the man yells �DAMN BRAT I�LL SUE!!!�  you lose $300 of today�s earnings";
                GameController.Money = GameController.Money - 3;
            }
            StartCoroutine(Scrolling());
        }
        else if (RandomEvent == 8)
        {
            if (CharacterInt <= 4)
            {
                TextStore = CharacterName + " Avoids all the red flags and retrieves the quarter, the toxic waste somehow doesn�t kill them and gives them superhuman strength +3 to strength.";
                CharacterStr = CharacterStr + 3;
            }
            else
            {
                TextStore = CharacterName + "is not as dumb as they look and refuses to retreve the quarter, Wharmongra dislikes your employee�s survival instincts being more powerful than their loyalty to you. -1 relationship";
                GameController.Relationship = GameController.Relationship - 2;
            }
            StartCoroutine(Scrolling());
        }
    }
    IEnumerator Scrolling()
    {
        foreach (char c in TextStore)
        {
            //adds the next character to the text every 0.04 seconds
            EventText.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        if (TextFinished == false)
        {
            TextFinished = true;
        }
        else if (TextFinished == true)
        {
            yield return new WaitForSeconds(0.75f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
    }
}
