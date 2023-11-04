using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HandPoint;
    public GameObject PickedObject = null;
    public bool canPickUp;
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
        /*this.GetComponent<BoxCollider>().isTrigger = true;
        
        if (Input.GetKeyDown(KeyCode.R) && PickedObject)
        {
            //this.gameObject.AddComponent<Rigidbody>();
            PickedObject.AddComponent<Rigidbody>();
            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
            PickedObject.gameObject.transform.SetParent(null);
            
            PickedObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && PickedObject !=null)
        {
           
            if(PickedObject!=null)
            {
                Destroy(this.GetComponent<Rigidbody>());
                Destroy(PickedObject.GetComponent<Rigidbody>());
                // PickedObject.GetComponent<Rigidbody>().useGravity = false;
                //PickedObject.GetComponent<Rigidbody>().isKinematic = true;
                PickedObject.gameObject.transform.SetParent(this.gameObject.transform);


                PickedObject.transform.position = HandPoint.transform.position;
            }
            


        }*/
        hitColliders = Physics.OverlapSphere(HandPoint.transform.position, radius, maskBox);
        PickedObject = null;
        foreach (var hitCollider in hitColliders)
        {
            
            PickedObject = hitColliders[hitColliders.Length-1].gameObject;
            break;
            
            



        }


        this.GetComponent<BoxCollider>().isTrigger = true;

        if (Input.GetKeyDown(KeyCode.R) &&PickedObject!=null&&!canPickUp)
        {
            //this.gameObject.AddComponent<Rigidbody>();
            PickedObject.AddComponent<Rigidbody>();
            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
            PickedObject.gameObject.transform.SetParent(null);
            canPickUp = true;
            PickedObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && PickedObject !=null&&canPickUp)
        {
           
            if(PickedObject!=null)
            {
                Destroy(this.GetComponent<Rigidbody>());
                Destroy(PickedObject.GetComponent<Rigidbody>());
                Vector3 originalScale = PickedObject.transform.localScale;

                PickedObject.gameObject.transform.SetParent(this.gameObject.transform);

                PickedObject.transform.localScale = originalScale;

                PickedObject.transform.position = HandPoint.transform.position;
                canPickUp = false;
            }
            


        }


    }

    /*private void OnTriggerStay(Collider other)
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
    }*/

}
