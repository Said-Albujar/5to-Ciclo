using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<GameObject> chainList;
    public ChangeCharacter changeMecanic;
    public bool isInsideCollider = false;

    public GameObject Plataforma;

    void Update()
    {
        if (changeMecanic.IsHairdress && isInsideCollider)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DeactivateChain();
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (changeMecanic.IsHairdress && collider.gameObject.tag == "Hair")
        {
            isInsideCollider = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (changeMecanic.IsHairdress)
        {
            isInsideCollider = false;
        }
    }

    private void DeactivateChain()
    {
        foreach (GameObject chain in chainList)
        {
            chain.SetActive(false);
        }
        Vector3 force = new Vector3(0,-5,0);
        Plataforma.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
