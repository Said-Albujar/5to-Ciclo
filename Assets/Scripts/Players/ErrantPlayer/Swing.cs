using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public GameObject ObjectToPickup;
    public GameObject PickedObject;
    public Transform InteractionZone;

    void Update()
    {
        if(ObjectToPickup != null && ObjectToPickup.GetComponent<PickableObject>().IsPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedObject = ObjectToPickup;
                PickedObject.GetComponent<PickableObject>().IsPickable = false;
                PickedObject.transform.SetParent(InteractionZone);
                PickedObject.transform.position = InteractionZone.position;
                //PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        } 

        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedObject = ObjectToPickup;
                PickedObject.GetComponent<PickableObject>().IsPickable = true;
                PickedObject.transform.SetParent(null);
               // PickedObject.GetComponent<Rigidbody>().useGravity = true;
                    PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;
            }
        }
    }
}
