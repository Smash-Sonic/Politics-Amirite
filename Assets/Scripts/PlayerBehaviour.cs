using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //mouse location = cam.ScreenToWorldPoint(Input.mousePosition
    public GameObject PickedUp;

    public Camera cam;
    public Coroutine HoldingObject;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hitCollider = Physics2D.OverlapBox(gameObject.transform.position, transform.localScale / 2, 0f);
            if (hitCollider != null)
            {
                if (hitCollider.name == "Person")
                {
                    HoldingObject = StartCoroutine(PickUp(hitCollider));
                }
                else if (hitCollider.name == "EndButton") 
                {
                    Debug.Log("end phase?");
                }
                //Debug.Log(hitCollider.name);
            }
        }
    }

    IEnumerator PickUp(Collider2D PickedUp) 
    {
        yield return new WaitForSeconds(0.01f);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(HoldingObject);
                break;
            }
            yield return new WaitForSeconds(0.01f);
            PickedUp.transform.position = transform.position;
        }
        yield return null;
    }
}
