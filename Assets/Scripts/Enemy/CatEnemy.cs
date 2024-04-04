using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CatEnemy : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    [Header("Cono de vision")]
    public bool canSeePlayer;
    public float radius;
    public LayerMask obstructionMask;
    public float angle;
    public bool rotating = true;
    public float rotationTimer;
    public float rotationMaxTimer;
    public float rotationEnemy;
    public CatEnemyAnimation catAnimator;
    private Vector3 firstPos;
    private Vector3 firstRotation;
    float timer;
    EnemyAudioManager enemyAudioManager;
    [SerializeField] CatArea catArea;
    [SerializeField] float timeOutOfView;
    bool inArea;

    private void Start()
    {
        catArea.PlayerInTheArea += () => StartView();
        catArea.PlayerOutTheArea += () => EndView();
        
        enemyAudioManager = GetComponent<EnemyAudioManager>();
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
    }
    private void Update()
    {
        if (canSeePlayer)
        {
            rotationTimer = 0f;
            navMeshAgent.isStopped = false;
            //El enemigo sigue al jugador en su rango
            if (playerPosition != null)
            {
                navMeshAgent.SetDestination(playerPosition.transform.localPosition);
                rotating = false;
                navMeshAgent.speed = 20f;
                
            }
        }
        else
        {
            catAnimator.anim.SetBool("run", false);
            navMeshAgent.SetDestination(firstPos);
            
            flipMove();
        }
    }

    void flipMove()
    {
        if (rotating)
        {
            //rota
            transform.rotation *= Quaternion.Euler(0f, (transform.rotation.y + rotationEnemy) * Time.deltaTime, 0f);
            rotationTimer += Time.deltaTime;

            // Si el temporizador alcanza la duraci�n deseada, detener la rotaci�n
            if (rotationTimer >= rotationMaxTimer)
            {
                rotationTimer = 0f;
                rotating = false;
            }

        }
        else
        {
            //detener la rotacion

            rotationTimer += Time.deltaTime;

            // Si el temporizador alcanza 2 segundos, reiniciar la rotaci�n
            if (rotationTimer >= rotationMaxTimer-1)
            {
                rotating = true;


                rotationTimer = 0f;
            }
        }
    }

    
    void StartView()
    { 
        inArea = true;
        StartCoroutine(FOVRoutine());
    } 
    void EndView()
    { 
        canSeePlayer = false;
        inArea = false;
    } 

    void FieldOfViewCheck()
    {
        Vector3 posPlayer = playerPosition.transform.position - transform.position;
        float distanceToTarget = Vector3.Distance(transform.position, playerPosition.transform.position);

        if (Vector3.Angle(transform.forward, posPlayer) < angle / 2 && distanceToTarget < radius)
        {
            if (!Physics.Raycast(transform.position, posPlayer, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
                inArea = false;
                enemyAudioManager.DetecPlayer();
            }
        }
    }

    IEnumerator CheckContinueInRangeView()
    {
        timer = 0;
        while(canSeePlayer)
        {
            
            Vector3 posPlayer = playerPosition.transform.position - transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, playerPosition.transform.position);

            if (Vector3.Angle(transform.forward, posPlayer) > angle / 2 && distanceToTarget < radius || 
            Physics.Raycast(transform.position, posPlayer, distanceToTarget, obstructionMask))
            {
                    yield return new WaitForSeconds(0.1f);
                    timer += 0.1f;
                    if(timer > timeOutOfView)
                    {
                        canSeePlayer = false;
                        StartView();
                    }
                
                
                yield return null;
            }
            else
            {
                yield return null;
                timer = 0;
            }
            yield return null;
        }
    }

    IEnumerator FOVRoutine()
    {
        while (inArea)
        {
            yield return null;
            FieldOfViewCheck();
            StartCoroutine(CheckContinueInRangeView());
        }
    }

    void LoadEnemy()
    {
        catArea.OnTriggerEnter(playerPosition.GetComponent<Collider>());
        catAnimator.anim.SetBool("spin", true);
        if (canSeePlayer == true)
            canSeePlayer = false;

        transform.position = firstPos;
        transform.rotation = Quaternion.Euler(firstRotation);

        rotationTimer = 0f;
        rotating = true;
        navMeshAgent.enabled = false;
        navMeshAgent.enabled = true;

    }
}
