using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWitcher : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public GameObject playerPosition;
    public bool patrullajeActivo = true;
    public float speedPlayerChange;
    [Header("Cono de vision")]
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public float time;
    public Collider[] rangeChecks;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
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
            }

        }
        else
        {
            patrullajeActivo = true;
            navMeshAgent.speed = 2f;
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
}
