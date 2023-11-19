using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MinerEnemy : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public bool patrullajeActivo = true;
    public float speedPlayerChange;
    public int nextStep = 0;
    public List<Transform> positionPoint;
    public float distanceMin;
    [Header("Cono de vision")]
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    private float time;
    public Collider[] rangeChecks;
    public bool once;
    public float timer;
    private Vector3 newDirection;

    private Vector3 firstPos;
    // Start is called before the first frame update
    private void Awake()
    {
        firstPos = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerPosition = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Start()
    {
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
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
                once = true;
                timer = 0f;
            }
            else
            {
                Debug.Log("Referencia nula en playerPosition");
            }

        }

        if (!canSeePlayer && once == true)
        {
            RandomMove();
        }

        if (!canSeePlayer && once == false)
        {
            patrullajeActivo = true;
            Patrol();
            navMeshAgent.speed = 4f;
        }


    }
    void Patrol()
    {
        navMeshAgent.SetDestination(positionPoint[nextStep].position);
        if (Vector3.Distance(transform.position, positionPoint[nextStep].position) < distanceMin)
        {
            nextStep++;
            if (nextStep >= positionPoint.Count)
            {
                nextStep = 0;
            }
        }
    }

    void RandomMove()
    {
        timer += Time.deltaTime;
        if (timer <= 3f)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                newDirection = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
                navMeshAgent.SetDestination(transform.position + newDirection);
            }
        }
        else
        {
            once = false;
            timer = 0f;
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

    void LoadEnemy()
    {
        if (canSeePlayer == true)
            canSeePlayer = false;
        if (once == true)
            once = false;
        transform.position = firstPos;
    }
}
