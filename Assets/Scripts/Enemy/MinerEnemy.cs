
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
    private bool onceShout;
    private float time;
    public Collider[] rangeChecks;
    public bool once;
    public float timer;
    private Vector3 newDirection;

    private Vector3 firstPos;
    private Vector3 firstRotation;
    private Vector3 firstPosModel;
    public Transform transformModel;
    public bool detecteEnemyLight;
    public float maxTimerStop;
     public float timerStop;
    public EnemyAudioManager enemyAudioManager;
    float timerSound;

    // Start is called before the first frame update
    private void Awake()
    {
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerPosition = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Start()
    {
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
        StartCoroutine(FOVRoutine());
        firstPosModel = new Vector3(0f, transformModel.localPosition.y, 0f);
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
                if (!onceShout)
                {
                    enemyAudioManager.DetecPlayer();
                    onceShout = true;
                }
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

        TimerDetecSound();


    }
    void StopMove()
    {
        navMeshAgent.isStopped = true;
       
    }
    void ResumeMove()
    {
        navMeshAgent.isStopped = false;
       
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
                newDirection = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                navMeshAgent.SetDestination(transform.position + newDirection);
            }
        }
        else
        {
            once = false;
            timer = 0f;
        }

    }

    private void TimerDetecSound()
    {
        if (onceShout && !canSeePlayer)
        {
            timerSound += Time.deltaTime;
            if (timerSound >= 0.25f)
            {
                onceShout = false;
                timerSound = 0f;
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

    void LoadEnemy()
    {
        if (canSeePlayer == true)
            canSeePlayer = false;
        if (once == true)
            once = false;
        transform.position = firstPos;
        transform.rotation = Quaternion.Euler(firstRotation);
        timer = 3f;
        //navMeshAgent.destination = Vector3.zero;
        //navMeshAgent.ResetPath();
        //transformModel.localPosition = firstPosModel;
        //navMeshAgent.isStopped = true;
        //navMeshAgent.speed = 0f;
        //navMeshAgent.Stop();
        navMeshAgent.enabled = false;

        //Reiniciar posici�n, resetear ruta, etc

        navMeshAgent.enabled = true;
    }
}
