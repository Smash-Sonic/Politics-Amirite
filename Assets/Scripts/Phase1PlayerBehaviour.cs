using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public GameObject[] EmployeeList;
    public int EmployeesOwned = 0;
    public int EmployeesDelegated = 0;
    public bool IsHolding = false;

    void Start()
    {
        EmployeeList = GameObject.FindGameObjectsWithTag("Person");
        for (int i = 0; i < EmployeeList.Length; i++)
        {
            Debug.Log(EmployeeList[i]);
        }
        EmployeesOwned = (EmployeeList.Length);
        Debug.Log(EmployeesOwned);
    }

    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        
        if (Input.GetMouseButtonDown(0))
        {
            //check for object to pick up or button to press
            HitColliders = null;
            HitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale / 2, 0f);

            for (int i = 0; i < HitColliders.Length; i++)
            {
                Debug.Log(HitColliders[i]);
            }

            if (IsHolding == false)
            {
                if (HitColliders[0] != null)
                {
                    if (HitColliders[0].tag == "Person")
                    {
                        IsHolding = true;
                        for (int i = 0; i < HitColliders.Length; i++)
                        {
                            if (HitColliders[i].tag == "Target")
                            {
                                EmployeesDelegated--;
                            }
                        }
                        HoldingObject = StartCoroutine(PickUp(HitColliders[0]));
                    }
                    else if (HitColliders[0].name == "EndButton")
                    {
                        //heck if all employees have been delegated
                        if (EmployeesDelegated >= EmployeesOwned)
                        {
                            Debug.Log("end phase?");
                        }
                        else
                        {
                            Debug.Log("Please Delegate all employees to a task.");
                        }
                    }
                    //Debug.Log(hitCollider.name);
                }
            }
            else if (IsHolding == true)
            {
                //let go of held object
                for (int i = 0; i < HitColliders.Length; i++)
                {
                    if (HitColliders[i].tag == "Target")
                    {
                        EmployeesDelegated++;
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
