using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public GameObject chain;
    public bool chainF;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "scissors" && Input.GetKeyDown(KeyCode.E))
        {
            chain.SetActive(false);
            chainF = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "scissors")
        {
            // Solo se llama una vez
            if (!chainF)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Solo se llama si el objeto está activo
                    if (gameObject.activeSelf)
                    {
                        Destroy(chain);
                    }
                }
            }
        }
    }
}
