using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Transform originalParent; // Almacenar el padre original del jugador

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        originalParent = transform.parent; // Almacenar el padre original al inicio
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Cuando el jugador colisiona con la plataforma, se convierte en hijo de la plataforma
            transform.parent = collision.transform;
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
