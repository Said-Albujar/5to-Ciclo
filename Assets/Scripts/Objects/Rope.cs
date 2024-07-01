using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public ChangeCharacter changeMecanic;
    public bool isInsideCollider = false;
    [SerializeField] CharacterJoint jointToCut;
    [SerializeField] GameObject box;
    [SerializeField] float force;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider localCollider;
    public GameObject cutVfxPrefab;
    public Transform cutPos;
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
            if (Input.GetKeyDown(KeyCode.E))
            {

                changeMecanic.anim.UsingToolAnim(2);
                ExecuteCut();
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

    [ContextMenu("EXECUTECUT")]
    public void ExecuteCut()
    {
        Vector3 directionFonce = transform.forward;
        directionFonce.y = 1;
        AudioManager.Instance.PlaySFX("cortar");
        box.GetComponent<Rigidbody>().isKinematic = false;
        box.GetComponent<Rigidbody>().useGravity = true;
        box.transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        localCollider.isTrigger = false;
        jointToCut.connectedBody = rb;
        rb.velocity = directionFonce * force;
        InstanstiateVFX();
        Destroy(gameObject);
    }

    private void InstanstiateVFX()
    {

        Instantiate(cutVfxPrefab, cutPos.position, Quaternion.identity);
    }
}
