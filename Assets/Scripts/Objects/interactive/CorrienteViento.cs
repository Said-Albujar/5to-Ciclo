using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrienteViento : MonoBehaviour
{
    public float airForce = 1.5f;
    public PlayerMovement player;
    public Vector3 direction;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hair") && player.glideDeployed)
        {
            player.ImpulseWithAir(airForce,direction);
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    // Verificar si el collider que entró en contacto es el jugador
    //    if (other.CompareTag("Hair"))
    //    {
    //        //player.FinalImpulse();
    //        //player.ascending = false;
    //        //player.impulsoInicial = Vector3.up * FuerzaViento;
    //        //player.FinalImpulse();
    //    }
    //}
}
