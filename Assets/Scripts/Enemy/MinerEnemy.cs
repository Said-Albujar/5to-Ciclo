using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MinerEnemy : MonoBehaviour
{
    public Transform playerPosition;
    public NavMeshAgent navMeshAgent;
    private int indexActualDestiny = 0;
    public Transform[] destinyPoints;
    public bool patrolActive;
    public float speedPlayerChange;
    public Vector3 direction;
    public float timer;
    public float speed;

    [Header("FOV")]
    public bool canSeePlayer;
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public float time;
    public Collider[] rangeChecks;
    public bool detecteEnemyLight;
    [HideInInspector] public float timerStop;
    public float maxTimerStop;
    public bool changeWait;

    private Vector3 firstPos;
    public enum EnemyState
    {
        patrol,
        wait,
        chase,
    }
    public EnemyState state;



    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        firstPos = transform.position;
    }

    void Start()
    {
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (state == EnemyState.chase && !canSeePlayer)
        {

            RandomMove();
        }

        if (detecteEnemyLight)
        {
            timerStop += Time.deltaTime;
            if (timerStop >= maxTimerStop)
            {
                detecteEnemyLight = false;
            }
            StopMove();
        }
        else
        {
            ResumeMove();
        }

        //
        if (canSeePlayer)
        {

            if (playerPosition != null)
            {
                state = EnemyState.chase;
                navMeshAgent.SetDestination(playerPosition.transform.position);
                navMeshAgent.speed = speedPlayerChange;
                patrolActive = false;
            }

        }
        if (!canSeePlayer && state == EnemyState.patrol)
        {

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                patrolActive = true;
                state = EnemyState.patrol;
                SetNextDestiny();
            }
        }



    }
    void StopMove()
    {
        navMeshAgent.isStopped = true;
    }
    void ResumeMove()
    {
        navMeshAgent.isStopped = false;
    }
    void RandomDirection()
    {
        float timer = 3f;
        float moveX = UnityEngine.Random.Range(-1f, 1f); // Genera un valor aleatorio en el eje x
        float moveZ = UnityEngine.Random.Range(-1f, 1f); // Genera un valor aleatorio en el eje z
        timer -= Time.deltaTime;
        if(timer<=3f)
        {
            direction = new Vector3(moveX, 0f, 0f);
            

        }
        if(timer<=1.5f)
        {
            direction = new Vector3(0f, 0f, moveZ);

        }


    }

    void RandomMove()
    {



        timer -= Time.deltaTime;
        if (timer >= 0f)
        {
            
            RandomDirection();
            
            navMeshAgent.SetDestination(transform.position + direction);
        }
        else if (timer <= 0f)
        {
            state = EnemyState.patrol;

            timer = 3f;
        }



        /*direction = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        navMeshAgent.SetDestination(transform.position + direction);
        if (timer >= 3f)
        {
            state = EnemyState.patrol;
        }

     }*/
    }


    void SetNextDestiny()
    {

        if (destinyPoints.Length == 0)
            return;

        navMeshAgent.destination = destinyPoints[indexActualDestiny].position;
        indexActualDestiny = (indexActualDestiny + 1) % destinyPoints.Length;
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
        transform.position = firstPos;
    }
}
