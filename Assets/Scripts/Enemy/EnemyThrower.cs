using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrower : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public float speedPlayerChange;
    public int nextStep = 0;
    public List<Transform> positionPoint;
    public float distanceMin;
    public Animator anim;

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
    [SerializeField] float rangeAttack;
    public int startBullets = 10;
    public int currentBullets;
    public Transform shootPoint;
    public float projectileSpeed;
    private float shootTimer;
    public float shootCooldown;
    private float minshootCooldown = 0.3f;

    private Vector3 firstPos;
    private Vector3 firstRotation;
    EnemyAudioManager enemyAudioManager;
    float timerSound;
    public GameObject[] needleObject;
    // Start is called before the first frame update
    private void Awake()
    {
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
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
        
    }



    private void Update()
    {
        if (canSeePlayer)
        {
            if (currentBullets>0)
            {
                if(Vector3.Distance(transform.position,playerPosition.transform.position) < radius)
                {
                    if(CheckInRangeView())
                    {
                        canSeePlayer = true;
                        Debug.Log("inrange");
                        if(Vector3.Distance(transform.position,playerPosition.transform.position) < rangeAttack)
                        {

                            navMeshAgent.enabled = false;
                            shootTimer += Time.deltaTime;

                            if (shootTimer >= shootCooldown)
                            {
                                anim.Play("Shoot");
                                //Shoot();
                                currentBullets -= 1;
                                shootTimer = 0f;
                                Debug.Log("Disparo");
                                for (int i = 0; i < needleObject.Length; i++)
                                {
                                    needleObject[i] = needleObject[currentBullets];
                                    Destroy(needleObject[i]);
                                }
                            }
                          

                            if (shootCooldown <= minshootCooldown)
                            {

                                shootCooldown = minshootCooldown;

                            }
                          


                            RotateToPlayer();
                        }
                        else
                        {
                            navMeshAgent.enabled = true;
                            navMeshAgent.SetDestination(playerPosition.transform.position);
                            
                        }
                    }
                    else
                    {
                        navMeshAgent.enabled = true;
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
                
            }

            else
            {
                if (playerPosition != null)
                {
                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(playerPosition.transform.position);
                    navMeshAgent.speed = speedPlayerChange;
                
                    once = true;
                    timer = 0f;
                    
                }
                else
                {
                    Debug.Log("Referencia nula en playerPosition");
                }
            }
            
        }
        else
        {
            FieldOfViewCheck();
            if (once == true)
            {
                RandomMove();
            }
            else
            {
                Patrol();
                navMeshAgent.speed = 4f;
            }
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

    public void Shoot()
    {
        Vector3 ofsset = new Vector3(0F, 0.15f, 0f);
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

    bool CheckInRangeView()
    {
        Vector3 player = new Vector3(playerPosition.transform.position.x,playerPosition.transform.position.y + 1f,playerPosition.transform.position.z);
        Vector3 posPlayer = player - transform.position;
        float distanceToTarget = Vector3.Distance(transform.position, playerPosition.transform.position);
        Debug.DrawRay(transform.position,posPlayer);
        if (Mathf.Abs(Vector3.Angle(transform.forward, posPlayer) - angle / 2) < 0.01f ||
        Physics.Raycast(transform.position, posPlayer, distanceToTarget, obstructionMask))
        {
            
            return false;
        }
        else
        {
            
            return true;
        }
    }


    void FieldOfViewCheck()
    {

        Vector3 player = new Vector3(playerPosition.transform.position.x,playerPosition.transform.position.y + 1f,playerPosition.transform.position.z);
        Vector3 posPlayer = player - transform.position;
        float distanceToTarget = Vector3.Distance(transform.position, playerPosition.transform.position);
        Debug.DrawRay(transform.position,posPlayer);
        if (Vector3.Angle(transform.forward, posPlayer) < angle / 2 && distanceToTarget < radius)
        {
            if (!Physics.Raycast(transform.position, posPlayer, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
                enemyAudioManager.DetecPlayer();
            }
        }
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

        //Reiniciar posici�n, resetear ruta, etc

        navMeshAgent.enabled = true;
    }
}
