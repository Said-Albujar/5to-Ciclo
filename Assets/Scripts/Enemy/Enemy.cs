using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public bool patrullajeActivo = true;
    public float speedPlayerChange;
    public int nextStep=0;
    public List<Transform> positionPoint;
    public float distanceMin;
    [Header("Cono de vision")]
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public float time;
    public Collider[] rangeChecks;
    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {

        StartCoroutine(FOVRoutine());
        
    }



    private void Update()
    {

        if (canSeePlayer)
        {
            if (playerPosition != null)
            {
                navMeshAgent.SetDestination(playerPosition.transform.position);
                navMeshAgent.speed = speedPlayerChange;
                patrullajeActivo = false;
            }

        }
        else
        {
            patrullajeActivo = true;
            MoveRandom();
            navMeshAgent.speed = 2f;
        }
    }
    void MoveRandom()
    {
        navMeshAgent.SetDestination(positionPoint[nextStep].position);
        if (Vector3.Distance(transform.position, positionPoint[nextStep].position)<distanceMin)
        {
            nextStep++;
            if(nextStep>=positionPoint.Count)
            {
                nextStep = 0;
            }
        }
    }
   
    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            FieldOfViewCheck();
        }
    }
    void FieldOfViewCheck()
    {

        rangeChecks = Physics.OverlapSphere(transform.position, radius, targeMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;

                }

                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
