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
    public static int LastEvent;
    public static int CharacterStr;
    public static int CharacterInt;
    public static int CharacterSoc;
    public static string CharacterName;
    public SpriteRenderer Renderer;
    public Sprite child;
    public Sprite grandma;
    public Sprite cleanup;
    public Sprite bleach;
    public Sprite elevator;
    public Sprite cop;
    public Sprite sale;
    public Sprite conman;
    public Sprite toxic;
    public Sprite Emmasary;
    public Sprite chigBungus;
    public AudioClip Click;
    public Collider2D[] HitColliders;
    public bool TextClicked;
    public bool AdvancePhase;

    void Start()
    {
        AdvancePhase = false;
        StartCoroutine(PhaseText());
    }
    IEnumerator PhaseText()
    {
        TextFinished = false;
        EventText.text = null;
        if (GameController.CurrentDay == 1)
        {
            LastEvent = 99;
            //tutorial stuff

            TextStore = null;
            TextFinished = false;

            TextStore = "During work hours, you'll be confronted with a random event that you'll need to delegate an employee to. It's important to interpret what stat is required to get the best outcome.";

            TextClicked = false;
            StartCoroutine(Scrolling());
            //yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => TextFinished == true);
        }

        TextFinished = false;
        EventText.text = null;
        if (GameController.CurrentDay < 7)
        {
            RandomEvent = Random.Range(1, 9);
                if (RandomEvent == LastEvent)
                {
                    while (RandomEvent == LastEvent)
                    {
                        //reroll
                        RandomEvent = Random.Range(1, 9);
                    }
                }
            if (RandomEvent == 1)
            {
                TextStore = null;
                TextStore = "A child is lost in the store and can’t find their parents, the child is extremely nervous and is on the brink of crying and screaming, send someone friendly to go with the child to find their parents.";
                Renderer.sprite = child;
            }
            else if (RandomEvent == 2)
            {
                TextStore = null;
                TextStore = "An old Woman tells customer service that she has lost her purse, send someone to help her find it.";
                Renderer.sprite = grandma;
            }
            else if (RandomEvent == 3)
            {
                TextStore = null;
                TextStore = "The Cleanup Company™ accepts a brand deal with your store. One of your employees needs to dress up as the company mascot: Captain Cleanup™ and stand outside the store to promote the new line of Cleanup detergent™.";
                Renderer.sprite = cleanup;
            }
            else if (RandomEvent == 4)
            {
                TextStore = null;
                TextStore = "There has been a spill of bleach in aisle 6. Send an employee to clean up the mess.";
                Renderer.sprite = bleach;
            }
            else if (RandomEvent == 5)
            {
                TextStore = null;
                TextStore = "A representative of Wharmongra arrives at the store to show off the superiority of their country, the diplomat challenges you to an arm wrestling contest, winning the contest would only provoke Wharmongra.";
                Renderer.sprite = Emmasary;
            }
            else if (RandomEvent == 6)
            {
                TextStore = null;
                TextStore = "A lawyer and his son are trapped in the elevator, send someone to fix the elevator.";
                Renderer.sprite = elevator;
            }
            else if (RandomEvent == 7)
            {
                TextStore = null;
                TextStore = "A con artist has entered the store, claiming that someone in the store owes them money, send someone to deal with them without being talkative.";
                Renderer.sprite = conman;
            }
            else if (RandomEvent == 8)
            {
                TextStore = null;
                TextStore = "Someone drops a quarter into a manhole, there is a green sludge at the bottom, the quarter is not worth it, only an idiot would go after it… But maybe an idiot is exactly what you need!";
                Renderer.sprite = toxic;
            }
            TextClicked = false;
            StartCoroutine(Scrolling());
        }
        else if(GameController.CurrentDay == 7)
        {
            //chig bungus events
            RandomEvent = Random.Range(1, 4);
            Renderer.sprite = chigBungus;
            if (RandomEvent == 1)
            {
                TextStore = null;
                TextStore = "Is that really him!? Chig Bungus himself has just walked through the door. He is the current ruler of Wharmongra and a ruthless customer. Looks like he’s buying a very heavy iron stove to cook his eggs, send an employee to lift it for him.";
            }
            else if (RandomEvent == 2)
            {
                TextStore = null;
                TextStore = "Is that really him!? Chig Bungus himself has just walked through the door. He is the current ruler of Wharmongra and a ruthless customer. He’s got blueprints for a complicated death machine, send an employee to help him find the right parts.";
            }
            else if (RandomEvent == 3)
            {
                TextStore = null;
                TextStore = "Is that really him!? Chig Bungus himself has just walked through the door. He is the current ruler of Wharmongra and a ruthless customer. It seems he needs help writing his latest political speech. Why did he come to the store for this? We may never know, just send an employee to help him.";
            }
            TextClicked = false;
            StartCoroutine(Scrolling());
        }
    }
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        
        if (ButtonPressed == true)
        {
            TextStore = null;
            EventText.text = null;
            ButtonPressed = false;
            AudioSource.PlayClipAtPoint(Click, camPos);
            StopCoroutine(Scrolling());
            ResultText();
        }

        if (Input.GetMouseButtonDown(0))
        {
            HitColliders = null;
            HitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale / 2, 0f);

            if (HitColliders.Length >= 1)
            {
                for (int i = 0; i < HitColliders.Length; i++)
                {
                    if (HitColliders[i].name == "TextBox")
                    {
                        if (TextClicked == false)
                        {
                            StopCoroutine(Scrolling());
                            TextClicked = true;

                            EventText.text = TextStore;
                        }
                        else
                        {
                            TextFinished = true;
                        }
                        if (AdvancePhase == true && TextFinished == true)
                        {
                            //next scene
                            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
                        }
                    }
                }
            }
        }
    }
    void ResultText()
    {
        TextFinished = false;
        if (GameController.CurrentDay < 7)
        {
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
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 2)
            {
                if (CharacterSoc >= 3)
                {
                    TextStore = null;
                    TextStore = CharacterName + " is able to locate the missing purse and is kind to the elderly woman, " + CharacterName + " is given a cookie as a reward from the elderly woman. +1 Relationship.";
                    GameController.Relationship++;
                }
                else if (CharacterSoc < 3)
                {
                    TextStore = null;
                    TextStore = CharacterName + " searches the entire store but cannot find the missing purse, " + CharacterName + " then has the “brilliant idea” to search the Wharmongran embassy, and they are later arrested for trespassing. -$100 in lost income and -1 relationship.";
                    GameController.Money--;
                    GameController.Relationship--;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 3)
            {
                if (CharacterStr >= 3)
                {
                    TextStore = CharacterName + " as Captain Cleanup™ is a huge hit with customers! The Cleanup Company™ gives you a bonus payment of $200.";
                    GameController.Money = GameController.Money + 2;
                }
                else if (CharacterStr < 3)
                {
                    TextStore = CharacterName + " as Captain Cleanup™ is a menace to society and scares away many potential customers. You lose $100 of your average earnings today.";
                    GameController.Money--;
                }
                TextClicked = false;
                AdvancePhase = true;
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
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 5)
            {
                if (CharacterStr <= 3)
                {
                    TextStore = CharacterName + " cannot beat the diplomat in a basic test of strength with their goofy little noodle arms. Wharmongra reaffirms their superiority to themselves +1 relationship.";
                    GameController.Relationship = GameController.Relationship + 1;
                }
                else if (CharacterStr > 3)
                {
                    TextStore = CharacterName + " wipes the floor with the diplomat, Wharmongra takes this as an act of aggression against their wonderful nation! -1 relationship.";
                    GameController.Relationship = GameController.Relationship - 1;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 6)
            {
                if (CharacterInt >= 4)
                {
                    TextStore = CharacterName + " successfully gets them out of the elevator, the lawyer gives you a big tip.";
                    GameController.Money++;
                }
                else if (CharacterInt < 9)
                {
                    TextStore = CharacterName + " is unable to get the two people out of the elevator, 5 hours later they are finally free. The pair both suffer brain damage. The lawyer is enraged and sues the store.";
                    GameController.Money--;
                }
                TextClicked = false;
                AdvancePhase = true;
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
                    TextStore = CharacterName + " approaches the strange man and says something kind to the man, before they can blink the man yells “DAMN BRAT I’LL SUE!!!”  you lose $200 of today’s earnings";
                    GameController.Money = GameController.Money - 2;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 8)
            {
                if (CharacterInt <= 4)
                {
                    TextStore = CharacterName + " avoids all the red flags and retrieves the quarter, the toxic waste somehow doesn’t kill them and gives them superhuman strength +3 to strength.";
                    CharacterStr = CharacterStr + 3;
                }
                else
                {
                    TextStore = CharacterName + " is not as dumb as they look and refuses to retreve the quarter, Wharmongra dislikes your employee’s survival instincts being more powerful than their loyalty to you. -1 relationship";
                    GameController.Relationship = GameController.Relationship - 1;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
        }
        else if (GameController.CurrentDay == 7)
        {
            if (RandomEvent == 1)
            {
                if (CharacterStr >= 8)
                {
                    TextStore = null;
                    TextStore = "Chig Bungus is impressed by your employee’s strength, as thanks he offers to cook an omelet for you, and you politely decline, assuring him that you mean no offense. + 4 relationship.";
                    GameController.Relationship = GameController.Relationship + 4;
                }
                else if (CharacterStr < 8)
                {
                    TextStore = null;
                    TextStore = CharacterName + " accidently drops the stove next to Chig Bungus, Wharmongra takes this as an act of aggression against their wonderful nation! -4 relationship.";
                    GameController.Relationship = GameController.Relationship - 4;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 2)
            {
                if (CharacterInt >= 8)
                {
                    TextStore = null;
                    TextStore = CharacterName + " purposely finds defective parts to ensure that Chig Bungus’s death machine won’t actually work but it will still look cool. He does not notice and thanks you for your service. +4 relationship.";
                    GameController.Relationship = GameController.Relationship + 4;
                }
                else if (CharacterInt < 8)
                {
                    TextStore = null;
                    TextStore = CharacterName + " cannot find any of the parts Chig Bungus is looking for… He’s not mad, just disappointed. -4 relationship.";
                    GameController.Relationship = GameController.Relationship - 4;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
            else if (RandomEvent == 3)
            {
                if (CharacterSoc >= 8)
                {
                    TextStore = CharacterName + " was able to assist Chig Bungus with their excellent public speaking skills, it’s almost a little worrying how they did so well on this particular task… +4 relationship.";
                    GameController.Relationship = GameController.Relationship + 4;
                }
                else if (CharacterSoc < 8)
                {
                    TextStore = CharacterName + " sucks at writing speeches. As a result, Chig Bungus’s speech is a disaster, but his approval rating suspiciously still goes up. He's upset regardless. -4 relationship.";
                    GameController.Relationship = GameController.Relationship - 4;
                }
                TextClicked = false;
                AdvancePhase = true;
                StartCoroutine(Scrolling());
            }
        }
    }
    IEnumerator Scrolling()
    {
        foreach (char c in TextStore)
        {
            //stop adding characters if has been clicked
            if (TextClicked == false)
            {
                //adds the next character to the text every 0.03 seconds
                EventText.text += c;
                yield return new WaitForSeconds(0.03f);
            }
        }

        if (AdvancePhase == true && TextFinished == true)
        {
            //next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }
    }
}
