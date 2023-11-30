
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MinerEnemy : MonoBehaviour
{
    public float timerSound;
    
    public EnemyAudioManager AudioMinerDetected;
    public Transform playerPosition;
    public NavMeshAgent navMeshAgent;
    private int indexActualDestiny = 0;
    public Transform[] destinyPoints;
    public bool patrolActive;
    public float speedPlayerChange;
    public Vector3 direction;
    public float timer;
    public float speed;
    public bool onceShout;
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
    public MinerAnimationController minerAnimation;
    bool once;
    Vector3 firstPos;
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
    }

    void Start()
    {
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
        firstPos = transform.position;
        StartCoroutine(FOVRoutine());
       
    }

    // Update is called once per frame
    void Update()
    {
        TimerDetecSound();
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
        if (canSeePlayer && !detecteEnemyLight)
        {

            if (playerPosition != null)
            {
                state = EnemyState.chase;
                navMeshAgent.SetDestination(playerPosition.transform.position);
                navMeshAgent.speed = speedPlayerChange;
                patrolActive = false;
                
                if (!onceShout)
                {
                    AudioMinerDetected.DetecPlayer();
                    onceShout = true;
                }
            }

        }
        if (!canSeePlayer && state == EnemyState.patrol)
        {
            navMeshAgent.speed = 4f;
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
        //minerAnimation.anim.speed = 0;
    }
    void ResumeMove()
    {
        navMeshAgent.isStopped = false;
        //minerAnimation.anim.speed = 1;

    }

    void RandomMove()
    {
        timer += Time.deltaTime;
        if (timer <= 3f)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                direction = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
                navMeshAgent.SetDestination(transform.position + direction);
            }
        }
        else
        {
            state = EnemyState.patrol;
            once = false;
            navMeshAgent.ResetPath();
            timer = 0f;
        }

    }
    private void TimerDetecSound()
    {
        if (onceShout && !canSeePlayer)
        {
            timerSound += Time.deltaTime;
            if (timer >= 0.25f)
            {
                onceShout = false;
                timerSound = 0f;
            }
        }

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
    void LoadEnemy()
    {
        if (canSeePlayer == true)
            canSeePlayer = false;
        if (once == true)
            once = false;
        transform.position = firstPos;
        timer = 3f;
        navMeshAgent.ResetPath();

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
                    if(!detecteEnemyLight)
                    {
                        canSeePlayer = true;

                    }
                    else
                    {
                        canSeePlayer = false;
                    }
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
