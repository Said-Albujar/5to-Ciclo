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
    public Animator animator;

    [Header("Cono de vision")]
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public float time;
    public Collider[] rangeChecks;

    [Header("Ataque")]
    public bool attack;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {

        if (canSeePlayer && !attack)
        {
            if (playerPosition != null)
            {

                    navMeshAgent.SetDestination(playerPosition.transform.position);
                    navMeshAgent.speed = speedPlayerChange;
                    patrullajeActivo = false;
                    animator.SetBool("walk", true);
            }
        }
        else
        {
            patrullajeActivo = true;
            navMeshAgent.speed = 2f;
            animator.SetBool("walk", false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            attack = true;
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            attack = false;
            animator.SetBool("walk", true);
            animator.SetBool("attack", false);
        }
    }
}
