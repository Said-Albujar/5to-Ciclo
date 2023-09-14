using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
     private Rigidbody rb;
    private bool InLadder = false;

    public float SpeedInLadder = 5.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            InLadder = true;
            rb.useGravity = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            InLadder = false;
            rb.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        if (InLadder)
        {
            float movimientoVertical = Input.GetAxis("Vertical");
            Vector3 movimiento = transform.up * movimientoVertical * SpeedInLadder * Time.fixedDeltaTime;
            rb.velocity = movimiento;
        }
    }
}
