using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase1PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public TMP_Text ResultsText;
    public int EmployeesDelegated = 0;
    public bool IsHolding = false;
    string[] EmployeeNames = new string[20];
    public GameObject TextBox;

    void Start()
    {
        //Instantiate(GameController.HiredEmployees[0]);
        //EmployeeList = GameObject.FindGameObjectsWithTag("Person");

        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            GameController.HiredEmployees[i].transform.position = new Vector3((i*2.5f)-7.25f, -2.5f, 0);
            GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
        }
        
        //EmployeesOwned = (EmployeeList.Length);
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
                                EmployeeNames[EmployeesDelegated] = HitColliders[h].name;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        return null;
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
}
