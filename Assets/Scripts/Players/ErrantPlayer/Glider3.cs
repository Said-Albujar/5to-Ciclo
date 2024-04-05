using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider3 : MonoBehaviour
{
    public bool GliderEquipped = false;
    public string status = "STATUS BASE";
    public float maxAscendSpeed = 10f; // Velocidad máxima ascendente
    public float gravityScaleFactor = 2f; // Factor para reducir la gravedad

    private Rigidbody rb;
    public KeyCode ascendKey = KeyCode.Space;
    public KeyCode toggleGliderItem = KeyCode.G;

    public bool isAscending = false;
    public bool isSlowFalling = true; // Cambiado a true para que la caída lenta sea predeterminada
    public float ascendForce = 20f; // Fuerza ascendente calculada

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Calculamos la fuerza ascendente requerida para contrarrestar la gravedad
       
        rb.useGravity = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleGliderItem))
        {
            GliderEquipped = !GliderEquipped;

        }
        if (GliderEquipped == true)
        {
            status = "GLIDER ACTIVO";
            isSlowFalling = true;
            if (Input.GetKeyDown(ascendKey))
            {
                isSlowFalling = false;
                isAscending = true;

            }

            if (Input.GetKeyUp(ascendKey))
            {
                isAscending = false;
                isSlowFalling = true;
            }

        }
        else
        {
            isAscending = false;
            isSlowFalling = false;
            status = "sin glider";
            rb.useGravity = true;
        }
    }

         
    void FixedUpdate()
    {
        if (isAscending)
        {
            rb.useGravity = true;
            isSlowFalling = false;
            // Aplica fuerza ascendente si la velocidad ascendente actual es menor que la velocidad máxima
            if (rb.velocity.y < maxAscendSpeed)
            {
                rb.AddForce(Vector3.up * ascendForce, ForceMode.Acceleration);
            }

        }

        if (isSlowFalling)
        {
            // Reducir la gravedad del Rigidbody según el factor definido por el usuario
            rb.useGravity = false; // Desactiva la gravedad normal
            rb.AddForce(Vector3.down * (Physics.gravity.magnitude / gravityScaleFactor), ForceMode.Acceleration); // Aplica una gravedad reducida
        }
        else
        {
            // Restablece la gravedad normal del Rigidbody
            rb.useGravity = true;
        }
    }
    
}
