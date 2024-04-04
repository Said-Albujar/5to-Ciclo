using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider2 : MonoBehaviour
{
    public float maxAscendSpeed = 10f; // Velocidad m�xima ascendente
    public float slowFallMultiplier = 0.1f; // Multiplicador de ca�da para la ca�da lenta
    public KeyCode ascendKey = KeyCode.Space;
    public KeyCode toggleSlowFallKey = KeyCode.G;

    private Rigidbody rb;
    private bool isAscending = false;
    private bool isSlowFalling = true; // Cambiado a true para que la ca�da lenta sea predeterminada
    private float ascendForce; // Fuerza ascendente calculada

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Calculamos la fuerza ascendente requerida para contrarrestar la gravedad ��FUNCIONA AL MISMO RADIO CON CUALQUIER PESO!!
        ascendForce = Physics.gravity.magnitude * rb.mass;
    }

    void Update()
    {
        if (Input.GetKeyDown(ascendKey))
        {
            isAscending = true;
        }
        if (Input.GetKeyUp(ascendKey))
        {
            isAscending = false;
        }

        if (Input.GetKeyDown(toggleSlowFallKey))
        {
            isSlowFalling = !isSlowFalling; // Cambia el estado de la ca�da lenta (activado/desactivado)
            Debug.Log("cambiogravitacional");
        }
    }

    void FixedUpdate()
    {
        if (isAscending)
        {
            // Aplica fuerza ascendente si la velocidad ascendente actual es menor que la velocidad m�xima
            if (rb.velocity.y < maxAscendSpeed)
            {
                rb.AddForce(Vector3.up * ascendForce, ForceMode.Acceleration);
            }
        }

        if (isSlowFalling)
        {
           
        }
    }
}
