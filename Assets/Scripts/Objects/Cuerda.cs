using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public GameObject[] objetosCuerda;
    public GameObject objetoQueCae;
    public string tijeraTag = "Tijera";

    private bool cuerdaRota = false;

    void Update()
    {
        // Verificar si la cuerda está rota
        if (!cuerdaRota)
        {
            foreach (GameObject cuerdaObjeto in objetosCuerda)
            {
                // Comprobar si la cuerda ha colisionado con un objeto con la etiqueta "Tijera"
                Collider collider = cuerdaObjeto.GetComponent<Collider>();
                if (collider != null && collider.CompareTag(tijeraTag))
                {
                    RomperCuerda();
                    return; // Terminar el bucle si se rompe una cuerda
                }
            }
        }
    }

    void RomperCuerda()
    {
        cuerdaRota = true;

        // Desactivar la física de las cuerdas
        foreach (GameObject cuerdaObjeto in objetosCuerda)
        {
            Rigidbody rb = cuerdaObjeto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        // Dejar caer el objeto que estaba sostenido por la cuerda
        Rigidbody objetoCaeRb = objetoQueCae.GetComponent<Rigidbody>();
        if (objetoCaeRb != null)
        {
            objetoCaeRb.isKinematic = false;
        }
    }
}
    