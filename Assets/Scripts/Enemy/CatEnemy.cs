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
    [SerializeField] Transform eyes;
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
    [SerializeField] float timeOutOfView;
    Vector3 lastPosPlayer;
    [HideInInspector] public bool runAnimation;
    [HideInInspector] public bool walkAnimation;


    private void Start()
    {
        enemyAudioManager = GetComponent<EnemyAudioManager>();
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
        DataPersistenceManager.instance.OnLoad += LoadEnemy;
    }
    private void Update()
    {
        if (canSeePlayer)
        {
            walkAnimation = false;
            rotationTimer = 0f;
            rotating = false;
            navMeshAgent.speed = 20f;
            //El enemigo sigue al jugador en su rango
            Vector3 player = new Vector3(playerPosition.transform.position.x,playerPosition.transform.position.y + 1f,playerPosition.transform.position.z);
            Vector3 posPlayer = player - eyes.position;
            float distanceToTarget = Vector3.Distance(eyes.position, playerPosition.transform.position);

            if (Mathf.Abs(Vector3.Angle(transform.forward, posPlayer) - angle / 2) < 0.01f || 
            Physics.Raycast(eyes.position + transform.forward * -1f, posPlayer, distanceToTarget, obstructionMask))
            {
                if(Vector3.Distance(lastPosPlayer,transform.position) <= 2)
                {
                    navMeshAgent.enabled = false;
                    runAnimation = false;
                    rotating = true;
                    flipMove();
                    timer += Time.deltaTime;

                    if(timer > timeOutOfView)
                    {
                        navMeshAgent.enabled = true;
                        canSeePlayer = false;
                        
                    }

                }
                else
                {
                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(lastPosPlayer);
                }
                
            }
            else
            {
                navMeshAgent.enabled = true;
                runAnimation = true;
                lastPosPlayer = playerPosition.transform.localPosition;
                navMeshAgent.SetDestination(playerPosition.transform.localPosition);
            }
        }
        else
        {
            FieldOfViewCheck();
            navMeshAgent.enabled = true;
            
            
            if(Vector3.Distance(transform.position,firstPos) > 1)
            {
                walkAnimation = true;
                navMeshAgent.SetDestination(firstPos);
            }
            else
            {
                walkAnimation = false;
                flipMove();
            }
            
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

    void FieldOfViewCheck()
    {
        
        Vector3 player = new Vector3(playerPosition.transform.position.x,playerPosition.transform.position.y + 1f,playerPosition.transform.position.z);
        Vector3 posPlayer = player - eyes.position;
        float distanceToTarget = Vector3.Distance(eyes.position, playerPosition.transform.position);
        Debug.DrawRay(eyes.position,posPlayer);
        if (Vector3.Angle(transform.forward, posPlayer) < angle / 2 && distanceToTarget < radius)
        {
            if (!Physics.Raycast(eyes.position, posPlayer, distanceToTarget, obstructionMask))
            {
                timer = 0;
                canSeePlayer = true;
                enemyAudioManager.DetecPlayer();
            }
        }
    }

    void LoadEnemy()
    {
        runAnimation = false;
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
