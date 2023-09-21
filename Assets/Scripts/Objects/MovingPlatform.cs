using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f;          // Velocidad de movimiento de la plataforma.
    public float distanceX = 5.0f;      // Distancia total que la plataforma se moverá en el eje X.
    public float distanceZ = 5.0f;      // Distancia total que la plataforma se moverá en el eje Z.
    public bool moveForwardAndBack = true; // Permite movimiento hacia adelante y hacia atrás.
    public bool moveLeftAndRight = false;  // Permite movimiento lateral en el eje Z.

    private Vector3 initialPoint;       // Posición inicial de la plataforma.
    private Vector3 endPointX;          // Posición final de la plataforma en el eje X.
    private Vector3 endPointZ;          // Posición final de la plataforma en el eje Z.
    private Vector3 targetPoint;        // Punto objetivo de movimiento.

    void Start()
    {
        initialPoint = transform.position;
        endPointX = transform.position + Vector3.right * distanceX;
        endPointZ = transform.position + Vector3.forward * distanceZ;
        targetPoint = endPointX;
    }

    void Update()
    {
        if (moveForwardAndBack)
        {
            // Calcula la dirección de movimiento en el eje X.
            Vector3 directionX = (targetPoint - transform.position).normalized;

            // Calcula la nueva posición de la plataforma en el eje X.
            Vector3 newPositionX = transform.position + directionX * speed * Time.deltaTime;

            // Comprueba si la plataforma ha llegado al punto objetivo en el eje X.
            if (Vector3.Distance(transform.position, targetPoint) <= 0.01f)
            {
                // Cambia la dirección en el eje X.
                targetPoint = (targetPoint == endPointX) ? initialPoint : endPointX;
            }

            // Mueve la plataforma hacia la nueva posición en el eje X.
            transform.position = newPositionX;
        }

        if (moveLeftAndRight)
        {
            // Calcula la dirección de movimiento en el eje Z.
            Vector3 directionZ = (endPointZ - transform.position).normalized;

            // Calcula la nueva posición de la plataforma en el eje Z.
            Vector3 newPositionZ = transform.position + directionZ * speed * Time.deltaTime;

            // Comprueba si la plataforma ha llegado al punto objetivo en el eje Z.
            if (Vector3.Distance(transform.position, endPointZ) <= 0.01f)
            {
                // Cambia la dirección en el eje Z.
                endPointZ = (endPointZ == (initialPoint + Vector3.forward * distanceZ)) ?
                    (initialPoint + Vector3.back * distanceZ) :
                    (initialPoint + Vector3.forward * distanceZ);
            }

            // Mueve la plataforma hacia la nueva posición en el eje Z.
            transform.position = newPositionZ;
        }
    }
}
