using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform originalParent; // Almacenar el padre original del jugador
    public float radius;
    public float distanceUp;
    public MovingPlatform1 movingPlatform;
    void Start()
    {
        originalParent = transform.parent; // Almacenar el padre original al inicio
    }
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position+(Vector3.up *distanceUp), Vector3.up, out hit, radius))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("MovingPlatform"))
            {
                movingPlatform = hit.collider.gameObject.GetComponent<MovingPlatform1>();
                movingPlatform.speed=0f;
            }
            else
            {
                if (movingPlatform != null)
                {
                    movingPlatform.speed = 2f;
                }
            }
        }
        else
        {
            if (movingPlatform != null)
            {
                movingPlatform.speed= 2f;
            }
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position + (Vector3.up * distanceUp), Vector3.up * radius);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Cuando el jugador colisiona con la plataforma, se convierte en hijo de la plataforma
            if(collision.transform.position.y < transform.position.y)
            {
                transform.parent = collision.transform;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Cuando el jugador deja de colisionar con la plataforma, vuelve a ser hijo de su padre original
            transform.parent = originalParent;
        }
    }
  

}
