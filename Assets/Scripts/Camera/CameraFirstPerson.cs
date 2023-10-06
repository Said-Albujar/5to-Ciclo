using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class CameraFirstPerson : MonoBehaviour
{
    [Header("Referencias")]
    public Transform pos_padre;
    public Transform pos_jugador;
    public Transform pos_orientacion;
    public CameraToggle camController;

    [Header("Coordenadas esféricas")]
    float azimutal;
    float polar;

    [Header("Movimiento de la Cámara")]
    public Vector2 sensibilidad;
    public float anguloPolarLimite;
    public float suavizado;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        camController = GameObject.FindObjectOfType<CameraToggle>();
    }

    void Update()
    {
        if (camController == null || camController.character == CameraToggle.Characters.Chef) return;

        //Input
        float x, y;

        x = Input.GetAxis("Mouse X");
        y = -Input.GetAxis("Mouse Y");

        //Cálculo ángulos
        azimutal = (azimutal + x * sensibilidad.x) % 360f;
        polar = Mathf.Clamp(polar + y * sensibilidad.y, -anguloPolarLimite, anguloPolarLimite);

        //Rotaciones y Posición de la cámara
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(polar, azimutal, 0f)), suavizado);
        pos_padre.position = pos_jugador.position;
        pos_orientacion.rotation = Quaternion.Euler(Vector3.up * transform.eulerAngles.y);
    }
}
