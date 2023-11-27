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
        if (other.gameObject.layer==LayerMask.NameToLayer("TargetPlayer"))
        {
            Debug.Log("asdasdasasd");
            // Cambiar a la nueva escena
            SceneManager.LoadScene(nombreDeLaNuevaEscena);
        }
    }
}
