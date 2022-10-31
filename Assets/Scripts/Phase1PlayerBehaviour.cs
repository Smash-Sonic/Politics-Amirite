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
    public GameObject[] EmployeesChosen = new GameObject[6];
    public TMP_Text ResultsText;
    public GameObject TextBox;
    public static bool TextFinished = false;
    public string TextStore;

    void Start()
    {
        //for setting employee positions
        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            GameController.HiredEmployees[i].transform.position = new Vector3((i*2.5f)-7.25f, -2.5f, 0);
            GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
        }

        //employee availability here

    }

    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        
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
                        if (EmployeesDelegated >= 5)
                        {
                            //Debug.Log("End Phase");
                            //GameController.Money += 4;

                            //text time
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
                TextStore = EmployeesChosen[0].name + " Carts complete, $100 earned";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = EmployeesChosen[0].name + " Carts fail";
            }
            StartCoroutine(Scrolling());
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
                TextStore = EmployeesChosen[1].name + " Stocking complete, $100 earned.";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = EmployeesChosen[1].name + " Stocking fail.";
            }
            StartCoroutine(Scrolling());
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
                TextStore = EmployeesChosen[2].name + " Register complete, $100 earned.";
                GameController.Money++;
            }
            else
            {
                //fail
                TextStore = EmployeesChosen[2].name + " Register fail.";
            }
            StartCoroutine(Scrolling());
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[3] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = EmployeesChosen[3].name + " Strength training complete, strength increased by 1.";
            //increase strength
            EmployeesChosen[3].gameObject.GetComponent<CharacterStats>().Strength++;

            StartCoroutine(Scrolling());
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[4] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = EmployeesChosen[4].name + " Intelligence training complete, intelligence increased by 1.";
            EmployeesChosen[4].gameObject.GetComponent<CharacterStats>().Intelligence++;
            //increase intelligence

            StartCoroutine(Scrolling());
            yield return new WaitUntil(() => TextFinished == true);
        }

        if (EmployeesChosen[5] != null)
        {
            TextFinished = false;
            ResultsText.text = null;

            TextStore = EmployeesChosen[5].name + " Social skills training complete, social skills increased by 1";
            EmployeesChosen[5].gameObject.GetComponent<CharacterStats>().SocialSkills++;
            //increase social skills

            StartCoroutine(Scrolling());
            yield return new WaitUntil(() => TextFinished == true);
        }

        //give money accordingly
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
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

    IEnumerator Scrolling()
    {
        foreach (char c in TextStore)
        {
            //adds the next character to the text every 0.05 seconds
            ResultsText.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        TextFinished = true;
    }
}
