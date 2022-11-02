using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        //arrange employees
        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            //Debug.Log(i);
            GameController.HiredEmployees[i].transform.position = new Vector3((i * 3f) - 4.25f, -0.5f, 0);
            GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
        }
        GameController.HiredEmployees[0].transform.position = new Vector3(1, 3.5f, 0);

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
                            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                        }
                        else if (GameController.CurrentDay >= 7)
                        {
                            //end the game
                            if (GameController.Relationship >= 5)
                            {
                                //best ending
                                MovieManager.MovieNumber = 3;
                            }
                            else if (GameController.Relationship >= 0) 
                            {
                                //good ending
                                MovieManager.MovieNumber = 4;
                            }
                            else if (GameController.Relationship <= -5)
                            {
                                //worst ending
                                MovieManager.MovieNumber = 1;
                            }
                            else if (GameController.Relationship <= -1)
                            {
                                //bad ending
                                MovieManager.MovieNumber = 2;
                            }
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
                                Destroy(HitColliders[i]);
                                Destroy(HitColliders[h]);
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
