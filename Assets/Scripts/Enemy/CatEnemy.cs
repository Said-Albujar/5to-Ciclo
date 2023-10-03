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
    public bool rotate;
    [Header("Cono de vision")]
    public bool canSeePlayer;
    public Collider[] rangeChecks;
    public float radius;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float angle;
    public float time=0;
    public float timer;
    public float rotationEnemy;
    private void Update()
    {
      
        if (canSeePlayer)
        {
            if (playerPosition != null)
            {
                navMeshAgent.SetDestination(playerPosition.transform.position);
                patrullajeActive = false;
            }

        }
        else
        {
            patrullajeActive = false;
            flipMove();
        }
    }
    private void Start()
    {
        
        StartCoroutine(FOVRoutine());

    }
    void flipMove()
    {
        Quaternion currentRotation = transform.rotation;
        if (rotate)
        {
            transform.rotation *= Quaternion.Euler(0f, rotationEnemy * Time.deltaTime, 0f);

        }
        else
        {
            //transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

        }
    }

    IEnumerator FOVRoutine()
    {



        flipMove();
        yield return new WaitForSeconds(timer);
        

        StartCoroutine(FOVRoutine());
        
    }
    void FieldOfViewCheck()
    {

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
