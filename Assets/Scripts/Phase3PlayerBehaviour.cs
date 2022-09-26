using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public Coroutine HoldingObject;
    public Collider2D[] HitColliders;
    public bool IsHolding = false;
    public bool Delegated = false;

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
            if (IsHolding == false)
            {
                if (HitColliders.Length >= 1)
                {
                    if (HitColliders[0].tag == "Money")
                    {
                        IsHolding = true;
                        HoldingObject = StartCoroutine(PickUp(HitColliders[0]));
                    }
                    else if (HitColliders[0].name == "EndButton")
                    {
                        //Debug.Log("End phase?");
                        if (GameController.CurrentDay <= 6)
                        {
                            GameController.CurrentDay++;
                            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                        }
                        else if (GameController.CurrentDay >= 7)
                        {
                            //end the game
                            Debug.Log("the game is over");
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
                        Delegated = true;
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
