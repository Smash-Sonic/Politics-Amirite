using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase1PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public int EmployeesDelegated = 0;
    public bool IsHolding = false;
    public bool IntroFinished = false;
    public GameObject[] EmployeesChosen = new GameObject[6];
    public TMP_Text ResultsText;
    public TMP_Text AvailabilityText;
    public GameObject TextBox;
    public static bool TextFinished = false;
    public string TextStore;
    public CharacterStats EmployeeShowUp;
    public int EmployeeAbsent = -1;
    public AudioClip Click;
    public SpriteRenderer Renderer;


    void Start()
    {
        //for setting employee positions

        for (int j = 0; j < GameController.HiredEmployees.Count; j++)
        {
            EmployeeShowUp = GameController.HiredEmployees[j].gameObject.GetComponent<CharacterStats>();
            EmployeeShowUp.ShowedUp = true;
        }

        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            //GameController.HiredEmployees[i].transform.position = new Vector3((i*2.5f)-7.25f, -2.5f, 0);
            GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
        }
        GameController.HiredEmployees[0].transform.position = new Vector3(3.65f, -3.45f, 0f);
        GameController.HiredEmployees[1].transform.position = new Vector3(3.8f, 1f, 0f);
        GameController.HiredEmployees[2].transform.position = new Vector3(6.9f, 1.9f, 0f);
        GameController.HiredEmployees[3].transform.position = new Vector3(2.4f, -0.6f, 0f);
        GameController.HiredEmployees[4].transform.position = new Vector3(5.2f, -1.2f, 0f);

        StartCoroutine(Availability());
    }

    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if(IntroFinished == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                //check for object to pick up or button to press
                HitColliders = null;
                HitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale / 2, 0f);

                //put all the touched colliders in debug log
                /*
                for (int i = 0; i < HitColliders.Length; i++)
                {
                    Debug.Log(HitColliders[i]);
                }
                */

                //for picking up an object
                //can only pick up if something is not already held and at least one collider was detected
                if (IsHolding == false && HitColliders.Length >= 1)
                {
                    for (int i = 0; i < HitColliders.Length; i++)
                    {
                        if (HitColliders[i].tag == "Person")
                        {
                            if (IsHolding == false)
                            {
                                IsHolding = true;
                                HoldingObject = StartCoroutine(PickUp(HitColliders[i]));
    
                                //for being picked up off of a target
                                /*if (HitColliders[i].tag == "Target")
                                {
                                    EmployeesDelegated--;
                                }*/
                            }
                        }
                        
                        else if (HitColliders[i].name == "EndButton")
                        {
                            //check if all employees have been delegated
                            if (EmployeesDelegated >= 4)
                            {
                                //Debug.Log("End Phase");
                                //GameController.Money += 4;
                                //text time
                                AudioSource.PlayClipAtPoint(Click, camPos);
                                Destroy(HitColliders[i]);
                                StartCoroutine(PhaseResults());
                            }

                            else
                            {
                                //Debug.Log("Please Delegate all employees to a task.");
                            }
                        }
                        
                    }
                }
                else if (IsHolding == true)
                {
                    //let go of held object

                    //for being dropped onto a target
                    for (int i = 0; i < HitColliders.Length; i++)
                    {
                        if (HitColliders[i].tag == "Target")
                        {
                            //check for which target, do stuff accordingly
                            //GameController.Money += 1;
                            for (int h = 0; h < HitColliders.Length; h++)
                            {
                                if (HitColliders[h].tag == "Person")
                                {
                                    if (HitColliders[i].name == "Carts")
                                    {
                                        //Debug.Log("hi");
                                        EmployeesChosen[0] = HitColliders[h].gameObject;
                                        //Debug.Log(EmployeesChosen[0]);
                                    }
                                    else if (HitColliders[i].name == "Stocking")
                                    {
                                        EmployeesChosen[1] = HitColliders[h].gameObject;
                                    }
                                    else if (HitColliders[i].name == "Register")
                                    {
                                        EmployeesChosen[2] = HitColliders[h].gameObject;
                                    }
                                    else if (HitColliders[i].name == "StrTraining")
                                    {
                                        EmployeesChosen[3] = HitColliders[h].gameObject;
                                    }
                                    else if (HitColliders[i].name == "IntTraining")
                                    {
                                        EmployeesChosen[4] = HitColliders[h].gameObject;
                                    }
                                    else if (HitColliders[i].name == "SocTraining")
                                    {
                                        EmployeesChosen[5] = HitColliders[h].gameObject;
                                    }
                                    EmployeesDelegated++;
                                    HitColliders[h].gameObject.transform.position = HitColliders[i].gameObject.transform.position;
                                    AudioSource.PlayClipAtPoint(Click, camPos);
                                    Destroy(HitColliders[i]);
                                    Destroy(HitColliders[h]);
                                }
                            }
                        }
                    }
                    IsHolding = false;
                    StopCoroutine(HoldingObject);
                }
            }
            else if (EmployeesDelegated >= 4)
            {
                Renderer.color = new Color(255, 255, 255, 255);
            }
        }
    }
    IEnumerator Availability()
    {
        if (GameController.CurrentDay == 1) 
        {
            //tutorial stuff

            TextBox.SetActive(true);
            AvailabilityText.gameObject.SetActive(true);

            AvailabilityText.text = null;
            TextStore = null;
            TextFinished = false;

            TextStore = "During the early morning, your job is to efficiently delegate employees to tasks. Make sure to check what each task entails before you assign an employee to it, then simply drag and drop an employee onto it. Sometimes, employees may not show up to work, and you'll be notified about it here.";

            StartCoroutine(Scrolling(false));
            yield return new WaitUntil(() => TextFinished == true);
        }
        TextFinished = false;
        //employee availability here
        if (GameController.CurrentDay > 1)
        {
            TextBox.SetActive(true);
            AvailabilityText.gameObject.SetActive(true);

            int RandomEmployeeEvent = Random.Range(0, 8);
            if (RandomEmployeeEvent <= 4)
            {
                //text stuff
                AvailabilityText.text = null;
                TextStore = null;
                int randomReason = Random.Range(0, 5);
                if(randomReason == 0)
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " was attacked by bees on the way to work and had to call off their shift due to allergies.";
                }
                else if(randomReason == 1)
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " was sent a threataning letter with a Wharmongran stamp in the mail and was told not to come to work today.";
                }
                else if (randomReason == 2)
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " took a wrong turn on their way to work and got lost in a different store, they didn't make it in time for work.";
                }
                else if (randomReason == 3)
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " didn't want to miss their favorite TV show so they called off their shift today.";
                }
                else if (randomReason == 4)
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " went on an incredibly profitable side adventure™, because of this they couldn't make it to work.";
                }
                else
                {
                    TextStore = GameController.HiredEmployees[RandomEmployeeEvent].name + " did not show up today.";
                }
                //code for making them actually not show up
                EmployeeShowUp = GameController.HiredEmployees[RandomEmployeeEvent].gameObject.GetComponent<CharacterStats>();
                EmployeeShowUp.ShowedUp = false;
                EmployeeAbsent = RandomEmployeeEvent;
                StartCoroutine(Scrolling(false));
                yield return new WaitUntil(() => TextFinished == true);
            }
            else if (RandomEmployeeEvent > 4)
            {
                //text stuff
                AvailabilityText.text = null;
                TextStore = null;
                TextStore = "Every employee showed up today.";
                StartCoroutine(Scrolling(false));
                yield return new WaitUntil(() => TextFinished == true);
            }
        }
        else
        {
            TextBox.SetActive(true);
            AvailabilityText.gameObject.SetActive(true);
            //text stuff
            AvailabilityText.text = null;
            TextStore = null;
            TextStore = "Every employee showed up on the first day.";
            StartCoroutine(Scrolling(false));
            yield return new WaitUntil(() => TextFinished == true);
        }

        TextBox.SetActive(false);
        AvailabilityText.gameObject.SetActive(false);
        IntroFinished = true;

        yield return null;
    }
    IEnumerator PhaseResults() 
    {
        TextBox.SetActive(true);
        ResultsText.gameObject.SetActive(true);

        //Debug.Log(EmployeesChosen[0]);
        if (EmployeesChosen[0] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            //Debug.Log(EmployeesChosen[0].gameObject.GetComponent<CharacterStats>().Strength);
            if (EmployeesChosen[0].gameObject.GetComponent<CharacterStats>().Strength >= 3)
            {
                //success
                TextStore = null;
                TextStore = EmployeesChosen[0].name + " carts complete, $100 earned.";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = null;
                TextStore = EmployeesChosen[0].name + " carts fail.";
            }
            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[1] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            //Debug.Log(EmployeesChosen[1].gameObject.GetComponent<CharacterStats>().Intelligence);
            if (EmployeesChosen[1].gameObject.GetComponent<CharacterStats>().Intelligence >= 3)
            {
                //success
                TextStore = null;
                TextStore = EmployeesChosen[1].name + " stocking complete, $100 earned.";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = null;
                TextStore = EmployeesChosen[1].name + " stocking fail.";
            }
            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[2] != null)
        {
            TextFinished = false;
            ResultsText.text = null;
            
            //Debug.Log(EmployeesChosen[2].gameObject.GetComponent<CharacterStats>().SocialSkills);
            if (EmployeesChosen[2].gameObject.GetComponent<CharacterStats>().SocialSkills >= 3)
            {
                //success
                TextStore = null;
                TextStore = EmployeesChosen[2].name + " register complete, $100 earned.";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = null;
                TextStore = EmployeesChosen[2].name + " register fail.";
            }
            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[3] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = null;
            TextStore = EmployeesChosen[3].name + " strength training complete, strength increased by 1.";
            //increase strength
            EmployeesChosen[3].gameObject.GetComponent<CharacterStats>().Strength++;

            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[4] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = null;
            TextStore = EmployeesChosen[4].name + " intelligence training complete, intelligence increased by 1.";
            EmployeesChosen[4].gameObject.GetComponent<CharacterStats>().Intelligence++;
            //increase intelligence

            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[5] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = null;
            TextStore = EmployeesChosen[5].name + " social skills training complete, social skills increased by 1.";
            EmployeesChosen[5].gameObject.GetComponent<CharacterStats>().SocialSkills++;
            //increase social skills

            StartCoroutine(Scrolling(true));
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (GameController.CurrentDay == 7)
        {
            //chig bungus entrance
            for (int j = 0; j < GameController.HiredEmployees.Count; j++)
            {
                GameController.HiredEmployees[j].SetActive(false);
            }
            MovieManager.MovieNumber = 5;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
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

    IEnumerator Scrolling(bool Source)
    {
        foreach (char c in TextStore)
        {
            //adds the next character to the text every 0.03 seconds
            if (Source == true)
            {
                ResultsText.text += c;
            }
            else if (Source == false)
            {
                AvailabilityText.text += c;
            }
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1f);
        TextFinished = true;
    }
}
