using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HandPoint;
    public GameObject PickedObject = null;
    private bool canPickUp;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && PickedObject != null)
        {
            this.gameObject.AddComponent<Rigidbody>().useGravity=false;
            PickedObject.AddComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeRotation;

            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
            PickedObject.gameObject.transform.SetParent(null);
            
            PickedObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && PickedObject !=null)
        {

            PickedObject.GetComponent<Rigidbody>().useGravity = false;
            PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            PickedObject.gameObject.transform.SetParent(this.gameObject.transform);
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(PickedObject.GetComponent<Rigidbody>());

            PickedObject.transform.position = HandPoint.transform.position;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box") && PickedObject == null)
        {
            PickedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box") && PickedObject)
        {
            PickedObject = null;
        }
    }

}
