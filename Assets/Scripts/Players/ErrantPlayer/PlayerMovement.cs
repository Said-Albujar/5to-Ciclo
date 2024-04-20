using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public CapsuleCollider cap;
    public static PlayerMovement Instance;
    public enum state
    {
        idle,
        moving,
        jumping,
        climbIdle,
        climbMoving,
        gliding
    }
    public state currentstate;

    [Header("Movement")]
    public float actualSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float pickBoxSpeed;
    public float groundDrag;
    public float rotationSpeed;
    [SerializeField] public bool isRunning;
    public bool turn;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;
    bool freeze;
    float horInput;
    float verInput;
    Vector3 moveDir;
    Rigidbody rb;
    Transform cameraTransform;

    [Header("JumpPlayer")]
    public float jumpForce;
    public float cooldownJump;
    public KeyCode jumpKey = KeyCode.Space;
    public bool isJump;

    [Header("Crouch")]
    public bool isCrouching;
    public float standHeight;
    public float standY;
    public float crouchHeight;
    public float CrouchY;
    public bool hold = true;

    [Header("Climb")]
    public LayerMask layerBorder;
    public float radius;
    public float UpDistance;
    public float GreenDistance;
    public float BlueDistance;
    Vector3 scaleStart;
    [SerializeField] bool greenLine;
    [SerializeField] bool blueLine;

    [Header("Glide")]
    public float MomentumKiller = 0.9f;
    public bool isGliding = false;

    public float contragravedad = 5.0f;

    public float planeonormal;
    public float contadort = 0f; //no recuerdo si us� estos 3
    //float lastInterpolationTime = 0f;

    public bool ascending = false;
    public PickUp pick;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        planeonormal = contragravedad;

        scaleStart = transform.localScale;
        if (MusicScene.Instance != null)
        {
            Destroy(MusicScene.Instance.gameObject);
        }
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SlowFalling()
    {
        //PLANEAAAAAAAAAAAAAARRRRRRR!!!!!!
        if (currentstate == state.gliding)
        {
            // rb.useGravity = false;
            //rb.velocity = Vector3.zero;
            rb.velocity *= MomentumKiller;

            //Vector3 reducedGravity = Physics.gravity / gravityScaleFactor;

            // rb.AddForce(reducedGravity, ForceMode.Acceleration);
            Vector3 verticalForce = new Vector3(0, contragravedad, 0);
            rb.AddForce(verticalForce, ForceMode.Force);
        }
    }


    void Update()
    {
        if (!ascending)
        {
            contragravedad = planeonormal;
        }

        //if (grounded)
        //{
        //    isGliding = false;
        //    currentstate = state.idle;
        //    rb.useGravity = true;
        //}
        //else
        //{
        //    GroundCheck();
        //}

        ActivateDesactivateGliding();

        switch (currentstate)
        {
            case state.climbIdle:

                CheckBorder();
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    currentstate = state.climbMoving;
                }
                break;

            case state.climbMoving:
                transform.position += Vector3.up * Time.deltaTime * UpDistance + transform.forward * 0.8f * Time.deltaTime;
                break;

            case state.gliding:
                SlowFalling();
                break;

            default:
                if (turn)
                {
                    NormalMovement();

                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (currentstate)
        {
            case state.climbIdle:
                break;
            case state.climbMoving:
                break;
            default:
                if (turn)
                {
                    MoveErrant();

                    RotatePlayer();
                }
                rb.useGravity = !freeze;
                break;
        }
    }

    void Jump()
    {
        if (freeze) return;

        if (Input.GetKeyDown(jumpKey) && grounded && !isCrouching && !GameManager.instance.inPause)
        {
            Invoke(nameof(JumpPlayer), cooldownJump);
            AudioManager.Instance.PlaySFX("Jump");
            grounded = false;
            isJump = true;
        }
    }
 
    void ErrantInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
        Jump();

        switch (hold)
        {
            case true:
                HoldCrouch();
                break;
            case false:
                PressCrouch();
                break;
        }
    }

    void ActivateDesactivateGliding()
    {
        if (Input.GetKeyDown(jumpKey) && !grounded && !GameManager.instance.inPause && currentstate != state.climbIdle)
        {
            switch (isGliding)
            {
                case true:
                    isGliding = false;
                    currentstate = state.idle;
                    //rb.useGravity = true;
                    break;
                case false:
                    isGliding = true;
                    currentstate = state.gliding;
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
        if (freeze) return;

        if (isCrouching)
        {
            cap.height = crouchHeight;
            cap.center = new Vector3(cap.center.x, CrouchY, cap.center.z);
        }
        else
        {
            cap.height = standHeight;
            cap.center = new Vector3(cap.center.x, standY, cap.center.z);
        }
    }
    void MoveErrant()
    {
        if (freeze) return;

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        moveDir = forward * verInput + right * horInput;

        if (Input.GetKey(KeyCode.LeftShift) && StaminaController.staminaActual >= 0 && StaminaController.canRun && moveDir.magnitude > 0)
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
        float radius = 0.25f;
        grounded = Physics.SphereCast(groundCheck.position, radius, Vector3.down, out RaycastHit hitInfo, playerHeight * 0.5f + 0.1f, ground);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = isRunning ? runSpeed : (isCrouching ? walkSpeed / 2 : (pick.haveObject ? pickBoxSpeed : walkSpeed));

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Drag()
    {
        if (grounded)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            turn = true;
            rb.drag = groundDrag;
        }
        else if (!grounded)
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

    public Vector2 GetPlayerInput()
    {
        return new Vector2(horInput, verInput);
    }

    public void SetFreeze(bool freeze)
    {
        this.freeze = freeze;
        if(freeze) rb.velocity = Vector3.zero;
    }

    private void HoldCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && grounded)
        {
            if (!isCrouching)
            {
                isCrouching = true;
                Crouch();
            }
        }
        else
        {
            if (isCrouching && !PlayerHeadCheck.headCheck)
            {
                isCrouching = false;
                Crouch();
            }
        }
    }

    private void PressCrouch()
    {
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
    public void FinishClimb()
    {
        rb.isKinematic = false;
        currentstate = state.idle;
    }
    void CheckBorder()
    {
        if(!grounded)
        {
            RaycastHit hit;
            blueLine = Physics.Raycast(groundCheck.position + (Vector3.up * BlueDistance), groundCheck.forward, out hit, radius, layerBorder);
            greenLine = Physics.Raycast(groundCheck.position + (Vector3.up * GreenDistance), groundCheck.forward, radius, layerBorder);
            
            if (blueLine && !greenLine  )
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                rb.useGravity = false;
                Vector3 dir = -hit.normal;
                dir.y = 0;
                
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.LookRotation(dir);
                if(hit.collider.gameObject.CompareTag("MovingPlatform"))
                {
                    transform.SetParent(hit.collider.transform);
                    Debug.Log("Plataforma");
                    currentstate = state.climbIdle;
                }
                else
                {
                    currentstate = state.climbIdle;

                }

            }
        
        }

        if(currentstate==state.climbIdle&&!greenLine&&!blueLine)
        {
            currentstate = state.idle;
            rb.isKinematic = false;
            rb.useGravity = true;
            
        }

        /* (!grounded)
        {
            blueLine = Physics.Raycast(groundCheck.position + (Vector3.up * BlueDistance), groundCheck.forward, out hit,radius, layerBorder);
            greenLine = Physics.Raycast(groundCheck.position + (Vector3.up * GreenDistance), groundCheck.forward, radius, layerBorder);
            if (blueLine && !greenLine)
            {
                transform.SetParent(hit.collider.transform);
             
                currentstate = state.climbIdle;
            }
           

        }*/


    }

    private void NormalMovement()
    {
        GroundCheck();
        SpeedControl();
        ErrantInput();
        CheckBorder();
        Drag();
        actualSpeed = rb.velocity.magnitude;
        if (grounded)
            isJump = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position + (Vector3.up * BlueDistance), groundCheck.forward * radius + groundCheck.position + (Vector3.up * BlueDistance));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position + (Vector3.up * GreenDistance), groundCheck.forward * radius + groundCheck.position + (Vector3.up * GreenDistance));
    }
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.hold = data.hold;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.hold = this.hold;
    }
}
