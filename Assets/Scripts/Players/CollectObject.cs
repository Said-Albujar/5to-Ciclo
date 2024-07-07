using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CollectObject : MonoBehaviour, IDataPersistence
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
    public GameObject vfxMejora;
    public GameObject panelUpgrade;
    public GameManager gameManager;
    private void Start()
    {
        ActualizarContadorTexto();
        ActualizarEstadoBoton();
    }

    void Update()
    {
        ActualizarContadorTexto();
        ActualizarEstadoBoton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("recolectable"))
        {
            contador++;
            ActualizarContadorTexto();
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX("Coins");
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
        }
    }

    void ActualizarContadorTexto()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text = "" + contador;
        }
    }

    public void ActualizarEstadoBoton()
    {
        if (miBoton != null)
        {
            miBoton.interactable = contador >= monedasNecesarias && !botonPresionado;
        }

        if (!regenerateStamina && contador >= costUpgradeCoin)
        {
            coinUpgrade.interactable = true;
        }
        else
        coinUpgrade.interactable = false;
        
    }

    public void OnBotonPresionado()
    {
        gameManager.Unpause();
        GameObject vfx=Instantiate(vfxMejora);
        AudioManager.Instance.PlaySFX("Improve");
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;

        vfx.transform.SetParent(transform);


        Debug.Log("�Bot�n presionado!");
        botonPresionado = true;
        contador -= monedasNecesarias;
        ActualizarEstadoBoton();
        
    }

    public void CoinButton()
    {
        if(contador <= costUpgradeCoin)
        return;

        gameManager.Unpause();
        GameObject vfx = Instantiate(vfxMejora);
        AudioManager.Instance.PlaySFX("Improve");

        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;

        vfx.transform.SetParent(transform);


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

    public void LoadData(GameData data)
    {
        this.contador = data.recolectedButtons;
    }

    public void SaveData(ref GameData data)
    {
        data.recolectedButtons = contador;
    }
}
