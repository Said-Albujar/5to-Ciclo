using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HandPoint;
    private GameObject PickedObject = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent <Rigidbody>().isKinematic = false;
            PickedObject.gameObject.transform.SetParent(null);
            PickedObject = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if(Input.GetKeyDown(KeyCode.E) && PickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = HandPoint.transform.position;
                other.gameObject.transform.SetParent(HandPoint.gameObject.transform);
                PickedObject = other.gameObject;
            }
        }
    }
}
