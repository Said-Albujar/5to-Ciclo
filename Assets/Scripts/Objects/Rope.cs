using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<GameObject> chainList;
    public ChangeCharacter changeMecanic;
    public bool isInsideCollider = false;
    public GameObject Plataforma;

    //Se cambio el fixed por update ya que daba demasiados problemas a la hora de cortar
    //Se añandio un bool que detecta la collider, si no esta dentro del colider no se destruye la cuerda.
    //Se añadio una lista,los objetos dentro de la lista se destruyen(cadenas).

    private void Start()
    {
        changeMecanic = FindObjectOfType<ChangeCharacter>();
    }
    void Update()
    {
        

        

        if (changeMecanic.IsHairdress && isInsideCollider)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                Debug.Log("SonidoCortar");
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
            
            AudioManager.Instance.PlaySFX("cortar");

            
        }
        Vector3 force = new Vector3(0,-5,0);
        Plataforma.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
