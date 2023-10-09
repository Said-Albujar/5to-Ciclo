using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBull : MonoBehaviour
{
    public float velocidad = 3.0f; // Velocidad del enemigo, configurable desde el Inspector.
    public float rangoDeSeguimiento = 10.0f; // Rango en el que el enemigo comenzará a seguir al objetivo.
    public Transform objetivo; // Objeto a seguir, configurado desde el Inspector.
    private NavMeshAgent agente;
    private bool enRango = false;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (objetivo == null)
        {
            Debug.LogError("Objetivo no asignado. Por favor, configure el objetivo en el Inspector.");
        }
        else
        {
            agente.speed = velocidad;
        }
    }

    private void Update()
    {
        if (objetivo != null)
        {
            float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivo.position);

            if (distanciaAlObjetivo <= rangoDeSeguimiento && objetivo.gameObject.activeSelf)
            {
                enRango = true;
            }
            else
            {
                enRango = false;
            }
        }
        if (enRango)
        {
            agente.SetDestination(objetivo.position);
        }
    }
}
