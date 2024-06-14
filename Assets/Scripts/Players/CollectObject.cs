using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CollectObject : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public int contador = 0;

    public Button miBoton; 
    [SerializeField] Button coinUpgrade;
    [SerializeField] int costUpgradeCoin;
    public int monedasNecesarias = 100;
    public bool botonPresionado = false;
    public bool botonCoin = false;

    public GameObject buttonVfxPrefab;

    static public  bool regenerateStamina = false;
    [SerializeField] float amountStamina; 


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
            AudioManager.Instance.PlaySFX("Coins");
            ActualizarEstadoBoton();
            InstanstiateVFX();

            if(regenerateStamina)
            {
                StaminaController.staminaActual += amountStamina;
            }
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

        if (coinUpgrade != null)
        {
            coinUpgrade.interactable = contador >= costUpgradeCoin && !botonCoin;
        }
    }

    public void OnBotonPresionado()
    {
        Debug.Log("�Bot�n presionado!");
        botonPresionado = true;
        contador -= monedasNecesarias;
        ActualizarEstadoBoton();
        
    }

    public void CoinButton()
    {
        regenerateStamina = true;
        botonCoin = true;
        contador -= costUpgradeCoin;
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
