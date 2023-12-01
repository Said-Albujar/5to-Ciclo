using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecolectarObjeto : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    private int contador = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("recolectable"))
        {
            contador++;

            ActualizarContadorTexto();

            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("recolectable"))
        {
            contador = Mathf.Max(0, contador);
            ActualizarContadorTexto();
        }
    }

    void ActualizarContadorTexto()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text = "" + contador;
        }
    }
}
