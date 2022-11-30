using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase3PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public GameObject MoneyReference;
    public CharacterStats EmployeeStats;
    public bool IsHolding = false;
    public bool Delegated = false;
    public bool MoneySpent = false;
    public SpriteRenderer Renderer;
    public AudioClip Click;
    public AudioClip Cash;

    public bool TutorialFinished = false;
    public int RandomEvent;
    public string TextStore;
    public TMP_Text TutorialText;
    public GameObject TextBox;
    public bool TextClicked;
    void Start()
    {
        if (GameController.CurrentDay == 1)
        {
            StartCoroutine(Tutorial());
        }
        else 
        {
            TutorialFinished = true;
        }
        if(GameController.CurrentDay == 7)
        {
            Renderer.color = new Color(255, 255, 255, 255);
        }
        //arrange employees
        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            //Debug.Log(i);
            if (GameController.HiredEmployees[i].GetComponent<BoxCollider2D>() == null)
            {
                GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
                GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
                GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
            }
        }
        GameController.HiredEmployees[0].transform.position = new Vector3(1, 3.35f, 0);
        GameController.HiredEmployees[1].transform.position = new Vector3(-2, 1.7f, 0);
        GameController.HiredEmployees[2].transform.position = new Vector3(2, 1.7f, 0);
        GameController.HiredEmployees[3].transform.position = new Vector3(5.5f, 1.25f, 0);
        GameController.HiredEmployees[4].transform.position = new Vector3(7.75f, -0.5f, 0);

        //give and arrange money

        for (int i = 0; i < GameController.Money; i++)
        {
            if (i < 3)
            {
                Instantiate(MoneyReference, new Vector3(i * 2.75f - 7.5f, -4, 0), Quaternion.identity);
            }
            else if (i < 6) 
            {
                Instantiate(MoneyReference, new Vector3(i * 2.75f - 15.75f, -2.5f, 0), Quaternion.identity);
            }
        }
    }
    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 camPos = Camera.main.transform.position;

        if (TutorialFinished == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //check for object to pick up or button to press
                HitColliders = null;
                HitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale / 2, 0f);

                if (IsHolding == false && HitColliders.Length >= 1)
                {
                    MoneySpent = false;
                    for (int i = 0; i < HitColliders.Length; i++)
                    {
                        if (HitColliders[i].tag == "Money")
                        {
                            if (IsHolding == false)
                            {
                                IsHolding = true;
                                HoldingObject = StartCoroutine(PickUp(HitColliders[0]));
                            }
                        }
                        else if (HitColliders[i].name == "EndButton")
                        {
                            //Debug.Log("End phase?");
                            if (GameController.CurrentDay <= 6)
                            {
                                GameController.CurrentDay++;
                                AudioSource.PlayClipAtPoint(Click, camPos);
                                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                            }
                            else if (GameController.CurrentDay >= 7)
                            {
                                //end the game
                                if (GameController.Relationship >= 15)
                                {
                                    //best ending
                                    MovieManager.MovieNumber = 3;
                                }
                                else if (GameController.Relationship >= 10)
                                {
                                    //good ending
                                    MovieManager.MovieNumber = 4;
                                }
                                else if (GameController.Relationship <= 0)
                                {
                                    //worst ending
                                    MovieManager.MovieNumber = 1;
                                }
                                else if (GameController.Relationship <= 5)
                                {
                                    //bad ending
                                    MovieManager.MovieNumber = 2;
                                }
                                for (int j = 0; j < GameController.HiredEmployees.Count; j++)
                                {
                                    //no more bozos hanging out on top of the end video
                                    Destroy(GameController.HiredEmployees[j]);
                                }
                                AudioSource.PlayClipAtPoint(Click, camPos);
                                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                            }
                        }
                    }
                }
                else if (IsHolding == true)
                {
                    //let go of held object
                    for (int i = 0; i < HitColliders.Length; i++)
                    {
                        if (HitColliders[i].tag == "Target")
                        {
                            //give money to wharmongra
                            GameController.Money--;
                            GameController.Relationship++;
                            for (int h = 0; h < HitColliders.Length; h++)
                            {
                                if (HitColliders[h].tag == "Money")
                                {
                                    AudioSource.PlayClipAtPoint(Cash, camPos);
                                    Destroy(HitColliders[h].gameObject);
                                }
                            }
                        }
                        if (HitColliders[i].tag == "Person")
                        {
                            //give money to employee
                            GameController.Money--;
                            //increase employee stats
                            if (MoneySpent == false)
                            {
                                EmployeeStats = HitColliders[i].gameObject.GetComponent<CharacterStats>();
                                EmployeeStats.Strength++;
                                EmployeeStats.Intelligence++;
                                EmployeeStats.SocialSkills++;
                                //Debug.Log("please don't run twice");
                            }
                            for (int h = 0; h < HitColliders.Length; h++)
                            {
                                if (HitColliders[h].tag == "Money")
                                {
                                    MoneySpent = true;
                                    Destroy(HitColliders[i]);
                                    Destroy(HitColliders[h]);
                                    AudioSource.PlayClipAtPoint(Cash, camPos);
                                }
                            }
                        }
                    }
                    IsHolding = false;
                    StopCoroutine(HoldingObject);
                }
            }
        }
        else
        {
            //clicking text here
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
                                TutorialText.text = TextStore;
                                TextClicked = true;
                            }
                            else
                            {
                                TextBox.SetActive(false);
                                TutorialText.gameObject.SetActive(false);
                                TutorialFinished = true;
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator PickUp(Collider2D PickedUp)
    {
        yield return new WaitForSeconds(0.01f);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            PickedUp.transform.position = transform.position;
        }
    }

    IEnumerator Tutorial()
    {
        TutorialText.gameObject.SetActive(true);
        TextBox.SetActive(true);
        TextStore = "For closing phase, you'll be able to distribute the store's earnings to your employees to improve their stats, or use some of your income to bribe Wharmongra and improve your relationship with them. This relationship is essential for determining the very fate of your store. Good luck!";
        StartCoroutine(Scrolling());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Scrolling()
    {
        foreach (char c in TextStore)
        {
            if (TextClicked == false)
            {
                //adds the next character to the text every 0.03 seconds
                TutorialText.text += c;
                yield return new WaitForSeconds(0.03f);
            }
        }
        yield return new WaitForSeconds(1f);
        //TextBox.SetActive(false);
        //TutorialText.gameObject.SetActive(false);
        //TutorialFinished = true;
    }
}
