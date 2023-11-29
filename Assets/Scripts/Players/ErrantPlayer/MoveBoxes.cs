using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    public float radius;
    public Transform posRaycast;
    public bool inBox;
    //public GameObject box;
    private void Start()
    {
        
    }
    private void Update()
    {
        RaycastHit hit;
         inBox = Physics.Raycast(posRaycast.transform.position, posRaycast.forward, out hit, radius);

        if(hit.collider.gameObject.CompareTag("BigBox"))
        {
            Debug.Log("col");
            inBox = true;
            hit.collider.gameObject.GetComponent<Rigidbody>().mass = 2;
            hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        }
        else
        {
            inBox = false;

            Debug.Log("noCol");

        }


    }
}
