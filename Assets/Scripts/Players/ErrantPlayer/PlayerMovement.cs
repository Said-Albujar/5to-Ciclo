using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public CapsuleCollider cap;
    [Header("Movement")]
    public float actualSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float groundDrag;
    public float rotationSpeed;
    [SerializeField] public bool isRunning;
    public bool turn;
    // public FallHairdresser fall;
    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;
    bool freeze;

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
    public bool isJump;

    [Header("Crouch")]
    public bool isCrouching;
    public float standHeight;
    public float standY;
    public float crouchHeight;
    public float CrouchY;
    public bool hold = true;
    public static PlayerMovement Instance;
    //[Header("Impulse")]
    //[SerializeField] GameObject stepRayUpper;
    //[SerializeField] GameObject stepRayLower;
    //[SerializeField] float stepHeight = 0.3f;
    //[SerializeField] float stepSmooth = 2f;
    //public float radius;
    //public RaycastHit hitUpper;
    //public RaycastHit hitLower;

    //[Header("Fall crash")]
    //public float fuerzaCaida;
    //public float radiusDrop;
    //public LayerMask maskCaida;
    //public bool wall;
    //public bool keyCheck;

    public enum state
    {
        idle,
        moving,
        jumping,
        climbIdle,
        climbMoving
    }
    public state currentstate;
    public LayerMask layerBorder;
    public Animator anim;
    public float radius;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (MusicScene.Instance != null)
        {
            Destroy(MusicScene.Instance.gameObject);

        }
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        // stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);

    }


    void Update()
    {

        switch (currentstate)
        {

            case state.climbIdle:
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("llegar", true);
                    anim.SetBool("inBorder", false);
                    currentstate = state.climbMoving;
                }
                break;

            case state.climbMoving:
                transform.position += Vector3.up * Time.deltaTime*2f+transform.forward*0.5f*Time.deltaTime;
                break;

            default:
                GroundCheck();
                SpeedControl();
                ErrantInput();
                CheckBorder();
                Drag();
                actualSpeed = rb.velocity.magnitude;
                if (grounded)
                {
                    isJump = false;

                }
                break;
        }




    }
    public void finishClimb()
    {
        rb.isKinematic = false;
        currentstate = state.idle;
        anim.SetBool("llegar", false);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
       
        Gizmos.DrawLine(transform.position, transform.forward * radius + transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + (Vector3.up * 1), transform.forward * radius + transform.position+(Vector3.up*1));

    }
    void CheckBorder()
    {
        bool blueLine = Physics.Raycast(transform.position, transform.forward, radius, layerBorder);
        bool greenLine = Physics.Raycast(transform.position + (Vector3.up * 1), transform.forward, radius, layerBorder);
        if (blueLine&&!greenLine)
        {
            currentstate = state.climbIdle;
            anim.SetBool("inBorder", true);
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
                //ImpulseElevator();

                rb.useGravity = !freeze;
                break;
        }


        //MoveErrant();


       
    }
   

  
    void Jump()
    {
        if (freeze) return;

        if (Input.GetKeyDown(jumpKey) && grounded && !isCrouching&&!GameManager.instance.inPause)
        {
            Invoke(nameof(JumpPlayer), cooldownJump);
            AudioManager.Instance.PlaySFX("Jump");
            grounded = false;
            isJump = true;
        }
    }
    void PlayWalkingSound()
    {
        if (!AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("caminar");
        }
    }

    void PlayRunningSound()
    {
        if (!AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("correr");
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
    void JumpPlayer()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        if(freeze) return;

        if (isCrouching)
        {
            //gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);


            //La parte comentada se usara cuando usemos un modelo 3d con animaciones


            cap.height = crouchHeight;
            cap.center = new Vector3(cap.center.x, CrouchY, cap.center.z);

        }
        else
        {
            //gameObject.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);




            //La parte comentada se usara cuando usemos un modelo 3d con animaciones


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
      
        /*if(verInput!=0 ||horInput!=0&&!isRunning)
        {
            //PlayWalkingSound();
        }*/
        
        if (Input.GetKey(KeyCode.LeftShift) && StaminaController.staminaActual >= 0 && StaminaController.canRun&&moveDir.magnitude>0)
        {
            
            isRunning = true;
            //PlayRunningSound();


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
        grounded = Physics.SphereCast(transform.position, radius, Vector3.down, out RaycastHit hitInfo, playerHeight * 0.5f + 0.1f, ground);
    }


    //void ImpulseElevator()
    //{
    //    RaycastHit hitLower;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, radius))
    //    {
    //        if (hitLower.collider.gameObject.CompareTag("Stairs"))
    //        {
    //            if (horInput != 0 || verInput != 0)
    //            {
    //                rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);

    //            }
    //        }
    //    }
    //}

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
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            turn = true;
            rb.drag = groundDrag;
        }
        else if (!grounded /*&& !fall.fall*/)
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

    public void TriggerJump()
    {
        JumpPlayer();
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
