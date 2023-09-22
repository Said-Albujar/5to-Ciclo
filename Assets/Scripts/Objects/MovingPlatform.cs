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

    private Vector3 initialPoint;      
    private Vector3 endPointX;         
    private Vector3 endPointZ;          
    private Vector3 targetPoint;        

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
            Vector3 directionX = (targetPoint - transform.position).normalized;
            Vector3 newPositionX = transform.position + directionX * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPoint) <= 0.01f)
            {
                targetPoint = (targetPoint == endPointX) ? initialPoint : endPointX;
            }
            transform.position = newPositionX;
        }

        if (moveLeftAndRight)
        {
            Vector3 directionZ = (endPointZ - transform.position).normalized;

            Vector3 newPositionZ = transform.position + directionZ * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, endPointZ) <= 0.01f)
            {
                endPointZ = (endPointZ == (initialPoint + Vector3.forward * distanceZ)) ?
                    (initialPoint + Vector3.back * distanceZ) :
                    (initialPoint + Vector3.forward * distanceZ);
            }
            transform.position = newPositionZ;
        }
    }
}
