using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase0PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public int EmployeesHired = 0;
    public bool IsHolding = false;

    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            //check for object to pick up or button to press
            HitColliders = null;
            HitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale / 2, 0f);
            /*
            for (int i = 0; i < HitColliders.Length; i++)
            {
                Debug.Log(HitColliders[i]);
            }
            */
            if (IsHolding == false && HitColliders.Length >= 1)
            {
                for (int i = 0; i < HitColliders.Length; i++)
                {
                    if (HitColliders[i].tag == "Person")
                    {
                        if (IsHolding == false)
                        {
                            IsHolding = true;
                            HoldingObject = StartCoroutine(PickUp(HitColliders[0]));

                            //for being picked up off of a target
                            /*if (HitColliders[i].tag == "Target")
                            {
                                EmployeesHired--;
                            }*/
                        }
                    }
                    else if (HitColliders[i].name == "EndButton")
                    {
                        //check if an employee has been delegated
                        if (EmployeesHired == 5)
                        {
                            //Debug.Log("End phase");
                            
                            for (int h = 0; h < GameController.HiredEmployees.Count; h++)
                            {
                                //prepare for sending to the next phase
                                DontDestroyOnLoad(GameController.HiredEmployees[h]);
                                //Debug.Log(GameController.HiredEmployees[h]);
                            }
                            
                            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                        }
                        else
                        {
                            //Debug.Log("Please Pick Employees to hire.");
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
                        EmployeesHired++;
                        for (int h = 0; h < HitColliders.Length; h++)
                        {
                            if (HitColliders[h].tag == "Person")
                            {
                                GameController.HiredEmployees.Add(HitColliders[h].gameObject);
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
