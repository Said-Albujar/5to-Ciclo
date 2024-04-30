using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public CapsuleCollider cap;
    public GameObject cap1;
    public static PlayerMovement Instance;
    public PlayerAnimationController playerAnimationController;
    public enum state
    {
        idle,
        moving,
        crouching,
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
    public bool isGliding = false;
    public bool canGlide = true;
    public bool gliderActive;
    public float planeo = 1f;
    public float antigravedad = 0.9f;

    public float actualGlideSpeed;
    public float topGlideSpeed;
    public float perdidadeinercia = 1f; // Tasa de disminución de velocidad (ajustable)
    public float perdidadeviento = 21f;
    public float perdidadeimpulso = 7f;

    public float gliderotationSpeed;
    public float glidedraft = 0.5f;

    public float planeonormal;

    public bool ascending = false;
    public float limiteinerciaviento = 60f;
    public PickUp pick;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        planeonormal = planeo;

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


   

    void Update()
    {
        GlideControl();

        switch (currentstate)
        {
            case state.climbIdle:
                isGliding = false;
                canGlide = false;
                CheckBorder();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerAnimationController.ClimbUpAnim();
                    currentstate = state.climbMoving;
                }
                break;

            case state.climbMoving:
                
                
                break;

            case state.gliding:
                
                SlowFalling();
                GlidingMovement();
                break;

            default:
                if (turn)
                {
                    rb.isKinematic = false;
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
                transform.position += Vector3.up * Time.deltaTime * UpDistance + transform.forward * 0.8f * Time.deltaTime;
                canGlide = false;
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
        canGlide = true;
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
       
        if (Input.GetKeyDown(jumpKey) && !grounded && !GameManager.instance.inPause && currentstate != state.climbIdle && isJump) //
        {
            topGlideSpeed = actualSpeed;

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
            cap1.SetActive(true);
            currentstate = state.crouching;
            cap.height = crouchHeight;
            cap.center = new Vector3(cap.center.x, CrouchY, cap.center.z);
        }
        else
        {
            cap1.SetActive(false);

            currentstate = state.idle;

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

        if (Input.GetKey(KeyCode.LeftShift) && StaminaController.staminaActual >= 0 && StaminaController.canRun && moveDir.magnitude > 0 && !isGliding)
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

    void GlideHorizontalSpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);            

        
        if (flatVel.magnitude > topGlideSpeed)
        {
            topGlideSpeed -= perdidadeinercia * Time.deltaTime;
            topGlideSpeed = Mathf.Max(topGlideSpeed, 0f);

            Vector3 limitedVel = flatVel.normalized * topGlideSpeed;
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
        if (moveDir != Vector3.zero && !isGliding)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        else if (moveDir != Vector3.zero && isGliding)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * gliderotationSpeed);
        }

    }
    void GlideControl()
    {
        if (!ascending && planeo > planeonormal)
        {

            planeo -= Time.deltaTime * perdidadeviento;
            planeo = Mathf.Max(planeo, 0f);
        }

        if (!ascending && planeo > limiteinerciaviento)
        {
            planeo = limiteinerciaviento;
        }

        if (!ascending && planeo < planeonormal)
        {
            planeo = planeonormal;
        }

        if (canGlide)
        {
            actualGlideSpeed = actualSpeed;
            gliderotationSpeed = rotationSpeed * glidedraft;
            if (gliderActive)
            {
                ActivateDesactivateGliding();

            }
        }
        else
        {
            planeo = planeonormal;
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
        canGlide = true;
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
                    canGlide = false;
                }
                else
                {
                    currentstate = state.climbIdle;
                    canGlide = false;

                }

            }
        
        }

        if(currentstate==state.climbIdle&&!greenLine&&!blueLine)
        {
            currentstate = state.idle;
            rb.isKinematic = false;
            rb.useGravity = true;

            canGlide = false;
        }
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
        {
            isJump = false;
            canGlide = false;
        }
        else if (!grounded)
        {
            isJump = true;
            canGlide = true;
        }
    }

    private void GlidingMovement()
    {
        GroundCheck();
        GlideHorizontalSpeedControl();
        ErrantInput();
        CheckBorder();
        Drag();
        actualSpeed = rb.velocity.magnitude;  // / 2  (reducir velocidad en el aire)

        if (grounded)
        {
            isJump = false;
            canGlide = false;
        }
        else if (!grounded)
        {
            isJump = true;
            canGlide = true;
        }

    }
    private Vector3 CalcularImpulsoInicial()
    {
        return Vector3.up * planeo;
    }


    public void SlowFalling()
    {
        if (grounded)
        {
            isGliding = false;
            currentstate = state.idle;
        }

        else
        {
            if (currentstate == state.gliding)
            {

                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * antigravedad, rb.velocity.z);

                Vector3 verticalForce = new Vector3(0, planeo, 0);
                rb.AddForce(verticalForce, ForceMode.Force);
            }
            GroundCheck();
        }
    }

    public void FinalImpulse()
    {
        rb.velocity *= Time.deltaTime * planeo / perdidadeimpulso;
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
