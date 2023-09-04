using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrantMovement : MonoBehaviour
{
    [Header("Movement")]
    public float actualSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float groundDrag;
    [HideInInspector]public bool isRunning;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    public Transform orientation;

    float horInput;
    float verInput;

    Vector3 moveDir;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

   
    void Update()
    {
        GroundCheck();
        SpeedControl();
        ErrantInput();
        Drag();
        actualSpeed = rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        MoveErrant();
    }

    void ErrantInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
    }

    void MoveErrant()
    {
        moveDir = orientation.forward * verInput + orientation.right * horInput;

        if (Input.GetKey(KeyCode.LeftShift) && StaminaController.staminaActual >= 0 && StaminaController.canRun)
        {
            isRunning = true;

        }
        else
        {
            isRunning = false;
        }

        switch (isRunning)
        {
            case true:
                rb.AddForce(moveDir.normalized * runSpeed * 10f, ForceMode.Force);
                break;
            case false:
                rb.AddForce(moveDir.normalized * walkSpeed * 10f, ForceMode.Force);
                break;
        }




    }

    void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        switch (isRunning)
        {
            case true:
                if (flatVel.magnitude > runSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * runSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
                break;
            case false:
                if (flatVel.magnitude > walkSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * walkSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
                break;
        }
        
    }

    void Drag()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
}
