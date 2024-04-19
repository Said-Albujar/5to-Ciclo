using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HandPoint;
    public GameObject PickedObject = null;
    public bool canPickUp;
    public bool haveObject;
    public static PickUp instance;
    public float radius;
    public Collider[] hitColliders;
    public LayerMask maskBox;
    public float moveSpeed = 3.0f;
    public Vector3 targetPosition;
    public bool box;
    public float timer, maxTimer;
    void Awake()
    {
        instance = this;

    }
    
    void Update()
    {
        RaycastHit hit;
        box=Physics.Raycast(transform.position, transform.forward, out hit,radius, maskBox);
        if(box)
        {
            PickedObject = hit.collider.gameObject;
        }
        else if(!box && !haveObject)
        {
            PickedObject = null;
        }

        if (haveObject)
        {
            float moveSpeed = 7.0f;
            Vector3 targetPosition = HandPoint.transform.position;
           
            timer += Time.deltaTime;

            if (timer<=maxTimer)
            {
               
            }
            else
            {
                if (PickedObject)
                {
                    PickedObject.transform.position = Vector3.Lerp(PickedObject.transform.position, HandPoint.transform.position, moveSpeed * Time.deltaTime);
                    if (PickedObject.transform.position == HandPoint.transform.position)
                    {
                        timer = 0f;

                    }
                }
                
              
            }
        }
        /*if (haveObject == false)
        {
            PickedObject = null;
            hitColliders = Physics.OverlapSphere(HandPoint.transform.position, radius, maskBox);
            foreach (var hitCollider in hitColliders)
            {

                PickedObject = hitColliders[hitColliders.Length - 1].gameObject;
                break;
            }
        }
        else
        {
            float moveSpeed = 3.0f;
            Vector3 targetPosition = HandPoint.transform.position; 

            //PickedObject.transform.position = Vector3.MoveTowards(PickedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            PickedObject.transform.position = Vector3.Lerp(PickedObject.transform.position, targetPosition, moveSpeed*Time.deltaTime);
        }*/
        

        

        if (Input.GetKeyDown(KeyCode.E) && PickedObject != null && !canPickUp)
        {
            PickedObject.AddComponent<Rigidbody>();
            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
            PickedObject.gameObject.transform.SetParent(null);
            canPickUp = true;
            PickedObject = null;
            haveObject = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && PickedObject != null && canPickUp)
        {
            timer = 0f;
            if (PickedObject != null)
            {
                Destroy(this.GetComponent<Rigidbody>());
                Destroy(PickedObject.GetComponent<Rigidbody>());
                Vector3 originalScale = PickedObject.transform.localScale;

                PickedObject.gameObject.transform.SetParent(this.gameObject.transform);

                //PickedObject.transform.localScale = originalScale;

                //PickedObject.transform.position = HandPoint.transform.position;
                canPickUp = false;
                haveObject = true;
            }



        }
        


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * radius);
    }
}
