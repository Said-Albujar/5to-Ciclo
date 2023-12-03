using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecolectarObjeto : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public int contador = 0;

    public Button miBoton; 
    public int monedasNecesarias = 100;
    public bool botonPresionado = false;

    public GameObject buttonVfxPrefab;

    private void Start()
    {
        ActualizarContadorTexto();
        ActualizarEstadoBoton();
    }

    public void Update()
    {
        ActualizarContadorTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("recolectable"))
        {
            contador++;
            ActualizarContadorTexto();
            other.gameObject.SetActive(false);
            ActualizarEstadoBoton();
            InstanstiateVFX();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("recolectable"))
        {
            contador = Mathf.Max(0, contador);
            ActualizarContadorTexto();
            ActualizarEstadoBoton();
        }
    }

    void ActualizarContadorTexto()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text = "" + contador;
        }
    }

    void ActualizarEstadoBoton()
    {
        if (miBoton != null)
        {
            miBoton.interactable = contador >= monedasNecesarias && !botonPresionado;
        }
    }

    public void OnBotonPresionado()
    {
        Debug.Log("¡Botón presionado!");
        botonPresionado = true;
        contador -= monedasNecesarias;
        ActualizarEstadoBoton();
        
    }

    private void InstanstiateVFX()
    {
        Vector3 currentPosition = transform.position;

        float offset = 1.3f;
        currentPosition.y += offset;

        Instantiate(buttonVfxPrefab, currentPosition, Quaternion.identity);
    }
}
