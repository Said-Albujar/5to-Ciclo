using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PassWordUI : MonoBehaviour
{
    public string nombreDeLaNuevaEscena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambiar a la nueva escena
            SceneManager.LoadScene(nombreDeLaNuevaEscena);
        }
    }
}
