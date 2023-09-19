using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float actualSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float groundDrag;
    public float rotationSpeed;
    [HideInInspector] public bool isRunning;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    public Transform orientation;

    float horInput;
    float verInput;

    Vector3 moveDir;

    Rigidbody rb;

    Transform cameraTransform;
    [Header("JumpPlayer")]
    public float jumpForce;
    public float cooldownJump;
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Impulse")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    public float radius;
    public RaycastHit hitUpper;
    public RaycastHit hitLower;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);

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
        RotatePlayer();
        ImpulseElevator();
    }

    void ErrantInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && grounded)
        {
            Invoke(nameof(JumpPlayer), cooldownJump);
            grounded = false;
        }
    }
    void JumpPlayer()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void MoveErrant()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        moveDir = forward * verInput + right * horInput;

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
    void ImpulseElevator()
    {



        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, radius))
        {                   
            if(hitLower.collider.gameObject.CompareTag("Stairs"))
            {
                if(horInput!=0 || verInput!=0)
                {
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);

                }
                else
                {
                    rb.transform.position = Vector3.zero;
                }
            }           
        }
       


        /*RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                if (hitLower45.collider.gameObject.CompareTag("Stairs"))
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                if (hitLowerMinus45.collider.gameObject.CompareTag("Stairs"))
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }*/
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

    void RotatePlayer()
    {
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
