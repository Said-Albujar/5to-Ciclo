using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrower : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public bool patrullajeActivo = true;
    public float speedPlayerChange;
    public int nextStep = 0;
    public List<Transform> positionPoint;
    public float distanceMin;

    public GameObject points;

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

    [Header("Throw")]
    public GameObject projectilePrefab;
    public int startBullets = 10;
    [HideInInspector]public int currentBullets;
    public Transform shootPoint;
    public float projectileSpeed;
    private float shootTimer;
    public float shootCooldown;
    private float minshootCooldown = 0.3f;

    private Vector3 firstPos;
    private Vector3 firstRotation;
    EnemyAudioManager enemyAudioManager;
    float timerSound;

    // Start is called before the first frame update
    private void Awake()
    {
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerPosition = FindObjectOfType<PlayerMovement>().gameObject;
        enemyAudioManager = GetComponent<EnemyAudioManager>();
    }
    private void Start()
    {
        foreach (Transform points in positionPoint)
        {
            points.parent = null;
        }
        currentBullets = startBullets;
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
        StartCoroutine(FOVRoutine());
    }



    private void Update()
    {
        if (canSeePlayer)
        {
            if (currentBullets>0)
            {
                shootTimer += Time.deltaTime;

                if (shootTimer >= shootCooldown)
                {
                    Shoot();
                    currentBullets -= 1;
                    shootTimer = 0f;
                    Debug.Log("Disparo");
                }

                if (shootCooldown <= minshootCooldown)
                    shootCooldown = minshootCooldown;

                RotateToPlayer();
            }

            else
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

    void Shoot()
    {
        Vector3 ofsset = new Vector3(0F, 0.25f, 0f);
        Vector3 directionToPlayer = (playerPosition.transform.position - shootPoint.position).normalized + ofsset;

        GameObject projectile = GameObject.Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = directionToPlayer * projectileSpeed;
        }
    }

    void RotateToPlayer()
    {
        Vector3 directionToPlayer = playerPosition.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        transform.rotation = targetRotation;
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

        navMeshAgent.enabled = false;

        //Reiniciar posición, resetear ruta, etc

        navMeshAgent.enabled = true;
    }
}
