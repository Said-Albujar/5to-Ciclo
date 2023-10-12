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
    public bool turn;
    public FallHairdresser fall;
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

    [Header("Crouch")]
    public bool isCrouching;
    public float standHeight;
    public float standY;
    public float crouchHeight;
    public float CrouchY;
    public CapsuleCollider[] capsuleColliders;

    [Header("Impulse")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    public float radius;
    public RaycastHit hitUpper;
    public RaycastHit hitLower;

    [Header("Fall crash")]
    public float fuerzaCaida;
    public float radiusDrop;
    public LayerMask maskCaida;
    public bool wall;

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
        FallWall();



        Drag();
        
        actualSpeed = rb.velocity.magnitude;

    }
    void FixedUpdate()
    {
        MoveErrant();
        if (turn)
        {
            RotatePlayer();
        }
        ImpulseElevator();
    }
    void FallWall()
    {
        wall = Physics.Raycast(transform.position, transform.forward, radiusDrop, maskCaida);
        /*if (wall&&!grounded)
        {
            rb.AddForce(Vector3.down * fuerzaCaida, ForceMode.Force);
        }*/

        if (wall && transform.position.y > 1f && !grounded)
        {
            rb.AddForce(Vector3.down * fuerzaCaida, ForceMode.Force);

        }
        else if (wall && grounded)
        {
            Jump();
        }
        else if (wall && verInput != 0)
        {
            rb.AddForce(Vector3.down * fuerzaCaida, ForceMode.Force); 
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * radiusDrop);
    }
    void Jump()
    {
        if (Input.GetKeyDown(jumpKey) && grounded && !isCrouching)
        {
            Invoke(nameof(JumpPlayer), cooldownJump);
            grounded = false;
        }
    }

    void ErrantInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");

        Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl) && grounded) 
        {
            switch (isCrouching)
            {
                case true:
                    if (!PlayerHeadCheck.headCheck) //Si hay algo encima del player, no se podra levantar
                    {
                        isCrouching = false;
                        Crouch();
                    }
                    break;
                case false:
                    {
                        isCrouching = true;
                        Crouch();
                    }
                    
                    break;
            }
        }
    }
    void JumpPlayer()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        if (isCrouching)
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);

            //La parte comentada se usara cuando usemos un modelo 3d con animaciones

            //foreach (var item in capsuleColliders)  
            //{
            //    item.height = crouchHeight;
            //    item.center = new Vector3(item.center.x, CrouchY, item.center.z);
            //}

        }
        else
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

            //La parte comentada se usara cuando usemos un modelo 3d con animaciones

            //foreach (var item in capsuleColliders)
            //{
            //    item.height = standHeight;
            //    item.center = new Vector3(item.center.x, standY, item.center.z);
            //}

        }
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
            if (hitLower.collider.gameObject.CompareTag("Stairs"))
            {
                if (horInput != 0 || verInput != 0)
                {
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);

                }
            }
        }
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
        if(grounded)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            turn = true;
            rb.drag = groundDrag;
        }
        else if(!grounded&&!fall.fall)
        {
            rb.drag = 0;
        } 
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
