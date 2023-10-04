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
    private void Update()
    {
        FieldOfViewCheck();
        if (canSeePlayer)
        {
            //El enemigo sigue al jugador en su rango
            if (playerPosition != null)
            {
                navMeshAgent.SetDestination(playerPosition.transform.position);
                patrullajeActive = false;
            }

        }
        else
        {
            //El enemigo ya no sigue al jugador
            patrullajeActive = false;
            flipMove();
        }
    }
   

    void flipMove()
    {
        if (rotate)
        {
            if (rotating)
            {
                //rota
                transform.rotation *= Quaternion.Euler(0f, rotationEnemy * Time.deltaTime, 0f);
                rotationTimer += Time.deltaTime;

                // Si el temporizador alcanza la duraci�n deseada, detener la rotaci�n
                if (rotationTimer >= rotationMaxTimer)
                {
                    rotating = false;
                    rotationTimer = 0f;
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
}
