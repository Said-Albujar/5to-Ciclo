using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinerEnemy : MonoBehaviour
{
    public float patrolSpeed = 2.0f;
    public float chaseSpeed = 4.0f;
    public float patrolRadius = 10.0f;
    public float chaseRadius = 5.0f;
    public float lightDisableTime = 3.0f;
    public Transform[] patrolPoints;
    public Transform player;
    public Light flashlight;
    public float maxLightDistance = 10.0f;
    public Transform flashlightDetectionPoint; // Asigna el GameObject delante de la linterna

    private enum EnemyState
    {
        Patrol,
        Chase,
        Disabled
    }

    private EnemyState currentState;
    private Vector3 originalPosition;
    private int currentPatrolPointIndex;
    private float disabledTimer;

    private void Start()
    {
        originalPosition = transform.position;
        currentState = EnemyState.Patrol;
        currentPatrolPointIndex = 0;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Disabled:
                Disabled();
                break;
        }

        // Comprobar si el jugador está dentro del radio de persecución
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
        {
            currentState = EnemyState.Chase;
        }

        // Comprobar si el enemigo está dentro del cono de luz de la linterna
        if (IsInLightCone())
        {
            currentState = EnemyState.Disabled;
        }
    }

    private bool IsInLightCone()
    {
        Vector3 toEnemy = transform.position - flashlightDetectionPoint.position;
        float angle = Vector3.Angle(toEnemy, flashlightDetectionPoint.forward);

        // Ajusta este ángulo según las características de la linterna y verifica la distancia
        if (angle < flashlight.spotAngle / 2f && Vector3.Distance(transform.position, flashlightDetectionPoint.position) <= maxLightDistance)
        {
            return true;
        }

        return false;
    }

    private void Patrol()
    {
        // Moverse hacia el punto de patrulla actual
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPointIndex].position, patrolSpeed * Time.deltaTime);

        // Comprobar si hemos llegado al punto de patrulla actual
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.1f)
        {
            // Moverse al siguiente punto de patrulla
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void Chase()
    {
        // Moverse hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Comprobar si el jugador ya no está dentro del radio de persecución
        if (Vector3.Distance(transform.position, player.position) > chaseRadius)
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void Disabled()
    {
        disabledTimer -= Time.deltaTime;
        if (disabledTimer <= 0)
        {
            currentState = EnemyState.Patrol;
        }
    }

    public void DisableMovement()
    {
        currentState = EnemyState.Disabled;
        disabledTimer = lightDisableTime;
    }
}
