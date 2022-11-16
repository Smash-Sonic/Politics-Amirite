using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase2PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public bool IsHolding = false;
    public bool Delegated = false;
    public CharacterStats ChosenStats;
    public AudioClip Click;


    void Start()
    {
        for (int i = 0; i < GameController.HiredEmployees.Count; i++)
        {
            //Debug.Log(i);
            GameController.HiredEmployees[i].transform.position = new Vector3((i * 2.5f) - 7.5f, 3f, 0);
            GameController.HiredEmployees[i].AddComponent<BoxCollider2D>();
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().size = new Vector2(2, 3.5f);
            GameController.HiredEmployees[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.5f);
        }
    }
    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 camPos = Camera.main.transform.position;

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
                                Delegated = false;
                            }*/
                        }
                    }
                    else if (HitColliders[i].name == "EndButton")
                    {
                        //check if an employee has been delegated
                        if (Delegated == true)
                        {
                            //Debug.Log("End phase");
                            if (Phase2TextBehaviour.TextFinished == true)
                            {
                                Phase2TextBehaviour.ButtonPressed = true;
                            }
                            //UnityEngine.SceneManagement.SceneManager.LoadScene(5);
                        }
                        else
                        {
                        Debug.Log("Please Delegate an employee to this task.");
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
                        Delegated = true;
                        for (int h = 0; h < HitColliders.Length; h++)
                        {
                            if (HitColliders[h].tag == "Person")
                            {
                                Destroy(HitColliders[i]);
                                Destroy(HitColliders[h]);
                                AudioSource.PlayClipAtPoint(Click, camPos);

                                //getting stats for the chosen employee
                                ChosenStats = HitColliders[h].gameObject.GetComponent<CharacterStats>();

                                Phase2TextBehaviour.CharacterName = HitColliders[h].name;
                                Phase2TextBehaviour.CharacterStr = ChosenStats.Strength;
                                Phase2TextBehaviour.CharacterInt = ChosenStats.Intelligence;
                                Phase2TextBehaviour.CharacterSoc = ChosenStats.SocialSkills;
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
