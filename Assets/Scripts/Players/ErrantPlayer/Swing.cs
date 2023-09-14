using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public Rigidbody rb;
    public bool estaSiendoAgarrado = false;
    private Vector3 posicionInicial;
    private bool estaCerca = false;
    private Transform jugador;

    public float distanciaMaxima = 2.0f;

    private Vector3 offset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        posicionInicial = transform.position;

        // Obtener la referencia al jugador (asume que el jugador tiene una etiqueta "Player")
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Verificar si el jugador está lo suficientemente cerca del objeto
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= distanciaMaxima)
        {
            estaCerca = true;

            if (Input.GetKeyDown(KeyCode.E) && !estaSiendoAgarrado)
            {
                estaSiendoAgarrado = true;
                rb.isKinematic = false;

                // Calcular el desplazamiento entre la posición del objeto y la posición del puntero del mouse en el momento de agarrar
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanciaAlJugador));
            }
        }
        else
        {
            estaCerca = false;
        }

        if (estaSiendoAgarrado)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                estaSiendoAgarrado = false;
                rb.isKinematic = true;
            }
            else
            {
                // Obtener la posición del mouse en el mundo y ajustar la posición del objeto en consecuencia
                Vector3 posicionMouseEnMundo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanciaAlJugador));
                rb.MovePosition(posicionMouseEnMundo + offset);
            }
        }
    }

    public void ReiniciarPosicion()
    {
        transform.position = posicionInicial;
    }
}
