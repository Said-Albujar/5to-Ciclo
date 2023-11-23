using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HandPoint;
    public GameObject PickedObject = null;
    public bool canPickUp;
    private bool haveObject;
    public static PickUp instance;
    public float radius;
    public Collider[] hitColliders;
    public LayerMask maskBox;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (haveObject == false)
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
            float moveSpeed = 20.0f;
            Vector3 targetPosition = HandPoint.transform.position; 

            PickedObject.transform.position = Vector3.MoveTowards(PickedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        

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
}
