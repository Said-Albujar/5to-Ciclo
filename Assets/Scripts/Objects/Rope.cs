using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject chain;
    public bool chainF;
    public ChangeCharacter changeMecanic;

    private void OnTriggerEnter(Collider collider)
    {
        if (changeMecanic.IsHairdress && collider.gameObject.tag == "Hair" && Input.GetKeyDown(KeyCode.F))
        {
            chain.SetActive(false);
            chainF = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (changeMecanic.IsHairdress && collider.gameObject.tag == "Hair")
        {
            // Solo se llama una vez
            if (!chainF)
            {
                if (Input.GetKeyDown(KeyCode.F))
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
