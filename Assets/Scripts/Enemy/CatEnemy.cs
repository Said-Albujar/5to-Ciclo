using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CatEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public bool patrullajeActive;
    public bool rotate = true;
    [Header("Cono de vision")]
    public bool canSeePlayer;
    public Collider[] rangeChecks;
    public float radius;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float angle;
    public bool rotating = true;
    public float rotationTimer;
    public float rotationMaxTimer;
    public float rotationEnemy;
    public CatEnemyAnimation catAnimator;
    public bool once;
    private float time = 0.2f;

    private Vector3 firstPos;
    private Vector3 firstRotation;
    private Vector3 firstPosModel;
    public Transform transformModel;
    private void Start()
    {
        firstPos = transform.position;
        firstRotation = transform.rotation.eulerAngles;
        firstPosModel = new Vector3(0f, transformModel.localPosition.y, 0f);
        DataPersistenceManager.instance.OnLoad += LoadEnemy;

        StartCoroutine(FOVRoutine());
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
                //catAnimator.anim.SetBool("run", true);
                navMeshAgent.SetDestination(playerPosition.transform.localPosition);
                rotating = false;
                navMeshAgent.speed = 20f;
                once = true;
            }

        }
        if (!canSeePlayer && once)
        {
            catAnimator.anim.SetBool("run", false);

            navMeshAgent.SetDestination(firstPos);
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                once = false;
            }
        }

        if (!canSeePlayer && !once)
        {
            flipMove();
            navMeshAgent.isStopped = true ;
        }
        
    }
   

    void flipMove()
    {
        if (rotate)
        {
            if (rotating)
            {
                //rota
                transform.rotation *= Quaternion.Euler(0f, (transform.rotation.y + rotationEnemy) * Time.deltaTime, 0f);
                rotationTimer += Time.deltaTime;

                // Si el temporizador alcanza la duración deseada, detener la rotación
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

                // Si el temporizador alcanza 2 segundos, reiniciar la rotación
                if (rotationTimer >= rotationMaxTimer-1)
                {
                    rotating = true;
                    //catAnimator.anim.SetBool("spin", rotating);

                    rotationTimer = 0f;
                }
            }
        }
       
    
    }
  
    
   
   
    void FieldOfViewCheck()
    {
        //El cono de vision 
        rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
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
        catAnimator.anim.SetBool("spin", true);
        if (canSeePlayer == true)
            canSeePlayer = false;

        if (once == true)
            once = false;

        transform.position = firstPos;
        transform.rotation = Quaternion.Euler(firstRotation);

        rotationTimer = 0f;
        rotating = true;
        transformModel.localPosition = firstPosModel;
        //navMeshAgent.destination = Vector3.zero;
        //navMeshAgent.ResetPath();
        //navMeshAgent.isStopped = true;
        //navMeshAgent.speed = 0f;
        navMeshAgent.enabled = false;

        //Reiniciar posición, resetear ruta, etc

        navMeshAgent.enabled = true;

    }
}
