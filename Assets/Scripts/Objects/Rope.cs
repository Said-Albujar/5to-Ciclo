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
    private void OnTriggerExit(Collider collider)
    {
        if (changeMecanic.IsHairdress && Input.GetKeyDown(KeyCode.Mouse0))
        {
            chain.SetActive(false);
            chainF = true;
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (changeMecanic.IsHairdress)
        {
            if (!chainF)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (gameObject.activeSelf)
                    {
                        Destroy(chain);
                    }
                }
            }
        }
    }

}
