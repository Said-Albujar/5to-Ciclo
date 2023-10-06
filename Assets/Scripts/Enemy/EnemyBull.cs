using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBull : MonoBehaviour
{
    [Header("Movement")]
    public float chaseSpeed = 5f;
    public float visionRange = 10f;
    public float chargeCooldown = 2f; 
   
    [Header("Break")]
    public float wanderCooldown = 5f; 
    public float wanderRadius = 10f; 

    private bool isWandering = false; 
    private float wanderTimer = 0f;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;
    private bool canCharge = true;
    private float chargeTimer = 0f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!isChasing)
        {
            if (CanSeePlayer())
            {
                StartChase();
            }
            else
            {
                if (!isWandering)
                {
                    StartWandering();
                }
                else
                {
                    wanderTimer += Time.deltaTime;
                    if (wanderTimer >= wanderCooldown)
                    {
                        StopWandering();
                    }
                }
            }
        }
        else if (canCharge)
        {
            Charge();
        }
        chargeTimer += Time.deltaTime;
        if (chargeTimer >= chargeCooldown)
        {
            canCharge = true;
        }
    }

    private bool CanSeePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Mata");

        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= visionRange)
            {
                player = p.transform;
                NavMeshHit hit;
                if (NavMesh.Raycast(transform.position, player.position, out hit, NavMesh.AllAreas))
                {
                    return false;
                }

                return true;
            }
        }

        return false;
    }

    private void StartChase()
    {
        navMeshAgent.speed = chaseSpeed;
        if (player != null)
        {
            isChasing = true;
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void Charge()
    {
        if (player != null)
        {
            NavMeshHit hit;
            if (NavMesh.Raycast(transform.position, player.position, out hit, NavMesh.AllAreas))
            {
                StopChase();
            }
            else
            {
                navMeshAgent.SetDestination(player.position);
                canCharge = false;
                chargeTimer = 0f;
            }
        }
    }

    private void StopChase()
    {
        isChasing = false;
        navMeshAgent.ResetPath();
    }

    private void StartWandering()
    {
        isWandering = true;
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        navMeshAgent.SetDestination(finalPosition);
    }

    private void StopWandering()
    {
        isWandering = false;
        wanderTimer = 0f;
        navMeshAgent.ResetPath();
    }
}