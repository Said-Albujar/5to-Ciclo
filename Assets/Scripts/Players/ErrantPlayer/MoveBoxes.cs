using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    public float radius;
    public Transform posRaycast;
    public GameObject[] box;
    public bool inBox;
    public float fuerzaEmpuje = 10f; // La fuerza con la que empujará el jugador
    public LayerMask layerBox;
    //public GameObject box;
    private void Start()
    {
        
    }


    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("BigBox")) 
        {
            Rigidbody cajaRigidbody = col.gameObject.GetComponent<Rigidbody>();

            if (cajaRigidbody != null)
            {
               
                Vector3 direccionEmpuje = col.gameObject.transform.position - transform.position;

               
                cajaRigidbody.AddForce(direccionEmpuje.normalized * fuerzaEmpuje, ForceMode.Impulse);
            }
        }
    }*/

    private void Update()
    {
        
        inBox=Physics.Raycast(posRaycast.transform.position, posRaycast.forward, radius, layerBox);   
    }
    private void FixedUpdate()
    {

        foreach (GameObject obj in box)
        {
            if (inBox)
            {
                Rigidbody rb2d = obj.GetComponent<Rigidbody>();

                if (rb2d != null)
                {

                    Vector3 direccionEmpuje = obj.transform.position - transform.position;


                    rb2d.AddForce(direccionEmpuje.normalized * fuerzaEmpuje * Time.fixedDeltaTime, ForceMode.Impulse);
                }
                obj.GetComponent<Rigidbody>().isKinematic = false;
            }
       










        }
    }


}

