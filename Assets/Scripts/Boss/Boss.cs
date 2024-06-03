using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    private IBossPhase currentPhase;
    [HideInInspector]public int actualPhase = 1;
    private NavMeshAgent agent;

    public Transform player;
    public FieldOfView fov;
    public bool active;
    public bool playerInGround;


    [Header("HeadRotation")]
    public Transform baseHead;
    public float maxRotationAngle = 30f;
    public float rotationSpeed;
    public float timeWait = 1f;
    private float timeSinceDirectionChange = 0f;
    private bool isLookingRight = true;

    [Header("Points")]
    public int indexPoint;
    public Transform[] phase1points;
    public Transform[] phase2points;
    public Transform[] phase3points;

    [Header("Attack")]
    public GameObject[] arms= new GameObject[2];
    private Animator animator;
    private PlayerHealth playerHealth;
    [HideInInspector]public bool isAttacking;
    [HideInInspector]public float timer;
    public GameObject prefabVfx;
    public GameObject[] vfxsPoint = new GameObject[2];
    public GameObject prefabLighting;
    public Transform lightingPoint;
    public float timeLightingAttack;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        instance = this;
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (playerHealth.health>0)
        {
            if (active)
            {
                animator.SetBool("isAttacking", isAttacking);
                switch (actualPhase)
                {
                    case 1:
                        currentPhase = new BossPhase1(this, agent, indexPoint);
                        break;
                    case 2:
                        currentPhase = new BossPhase2(this, agent, indexPoint);
                        break;
                    case 3:
                        currentPhase = new BossPhase3(this, agent, indexPoint);
                        break;
                    default:
                        break;
                }
                currentPhase.Execute();
            }
        }
       
        
    }

    public void LookAround()
    {
        if (player)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(direction);
            if (Quaternion.Angle(transform.rotation, playerRotation) < 0.2f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, 40 * Time.deltaTime);
            }
            else
            {
                RotateHead();
            }
               
        }
        
    }

    public void RotateHead()
    {
        if (timeSinceDirectionChange >= timeWait)
        {
            float targetAngle = isLookingRight ? maxRotationAngle : -maxRotationAngle;

            Vector3 currentLocalEulerAngles = baseHead.localRotation.eulerAngles;

            Vector3 targetLocalEulerAngles = new Vector3(0, targetAngle, currentLocalEulerAngles.z);

            Quaternion targetRotation = Quaternion.Euler(targetLocalEulerAngles);

            Quaternion interpolatedRotation = Quaternion.Lerp(baseHead.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 interpolatedEulerAngles = interpolatedRotation.eulerAngles;
            interpolatedEulerAngles.x = 0;
            baseHead.localRotation = Quaternion.Euler(interpolatedEulerAngles);

            if (Quaternion.Angle(baseHead.localRotation, targetRotation) < 0.1f)
            {
                isLookingRight = !isLookingRight;
                timeSinceDirectionChange = 0f;
            }
        }
        else
        {
            timeSinceDirectionChange += Time.deltaTime;
        }


    }

    public void RotateHeadToPlayer()
    {
        Vector3 direction = player.position - baseHead.position;
        Quaternion playerRotation = Quaternion.LookRotation(direction);

            baseHead.rotation = Quaternion.Lerp(baseHead.rotation, playerRotation, 40 * Time.deltaTime);

            
    }
    public void RotateBodyToDesk(float yRotation)
    {
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;

        Vector3 targetEulerAngles = new Vector3(currentEulerAngles.x, yRotation, currentEulerAngles.z);

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 40 * Time.deltaTime);
    }

    public void Attack(int attackId)
    {
        if (!arms[0].activeSelf)
        {
            foreach (GameObject arm in arms)
            {
                arm.SetActive(true);
            }
        }
        
        switch (attackId)
        {
            case 1:
                animator.Play("BossAttack1");
                break;
            case 2:
                animator.Play("BossAttack2");
                break;
            case 3:
                animator.Play("BossAttack3");
                break;

        }
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
    public void KillPlayer()
    {
        if (timer <= timeLightingAttack)
        {
            playerHealth.activateVfx = true;
            Debug.Log(timer);
            timer += Time.deltaTime;
        }
        else
        {
            InstantiateLighting();
            playerHealth.activateVfx = false;
            playerHealth.health = 0;
        }
    }

    public void StopVfxPlayer()
    {
        playerHealth.DisableVfxDeathByBoss();
    }
    public void DesactiveArms()
    {

        foreach (GameObject arm in arms)
        {
            arm.SetActive(false);
        }
    }
    public void InstantiateLighting()
    {
        Instantiate(prefabLighting, lightingPoint.position, Quaternion.identity);
    }
    public void PlayLeftVfx()
    {
        Instantiate(prefabVfx, vfxsPoint[0].transform.position, Quaternion.identity);
    }

    public void PlayRightVfx()
    {
        Instantiate(prefabVfx, vfxsPoint[1].transform.position, Quaternion.identity);
    }
}
