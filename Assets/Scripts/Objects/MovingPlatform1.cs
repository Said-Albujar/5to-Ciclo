using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    public Transform pointA;    // Transform del punto A
    public Transform pointB;    // Transform del punto B
    public float speed = 2.0f; // Velocidad de la plataforma

    private Vector3 target;     // El punto hacia el que se está moviendo

    void Start()
    {
        // Al inicio, la plataforma se encuentra en el punto A
        target = pointB.position;
    }

    void Update()
    {
        // Mueve la plataforma hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Si la plataforma llega a uno de los puntos, cambia el objetivo al otro punto
        if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
        {
            target = pointB.position;
        }
        else if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
        {
            target = pointA.position;
        }
    }
}
