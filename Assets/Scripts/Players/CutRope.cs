using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    [SerializeField] CharacterJoint jointToCut;
    [SerializeField] GameObject box;
    [SerializeField] float force;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider localCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        localCollider = GetComponent<Collider>();
    }

    [ContextMenu("EXECUTECUT")]
    public void ExecuteCut(Transform dir)
    {
        Vector3 directionFonce = dir.forward;
        directionFonce.y = 1;
        
        box.GetComponent<Rigidbody>().isKinematic = false;
        box.GetComponent<Rigidbody>().useGravity = true;
        box.transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        localCollider.isTrigger = false;
        jointToCut.connectedBody = rb;
        rb.velocity = directionFonce * force;
    }
}
