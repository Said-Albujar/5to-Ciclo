using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private IBossPhase currentPhase;
    [HideInInspector]public int actualPhase = 1;
    private NavMeshAgent agent;

    public Transform player;
    public FieldOfView fov;
    public bool active;

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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (active)
        {
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

            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            baseHead.rotation = Quaternion.Lerp(baseHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(baseHead.rotation, targetRotation) < 0.1f)
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

    public void RotateToPlayer()
    {
        Vector3 direction = player.position - baseHead.position;
        Quaternion playerRotation = Quaternion.LookRotation(direction);

            baseHead.rotation = Quaternion.Lerp(baseHead.rotation, playerRotation, 40 * Time.deltaTime);

            
    }
}
