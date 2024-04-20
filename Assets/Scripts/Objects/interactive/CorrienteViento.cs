using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrienteViento : MonoBehaviour
{
    // Start is called before the first frame update

    public int FuerzaViento = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public PlayerMovement player;


    //private void OnTriggerEnter(Collider other)
    //{
    //    // Verificar si el collider que entró en contacto es el jugador
    //    if (other.CompareTag("Player"))
    //    {           
    //        player.contragravedad = FuerzaViento;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el collider que entró en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            player.planeo = FuerzaViento;
            player.ascending = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el collider que entró en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            player.ascending = false;
        }
    }
}
