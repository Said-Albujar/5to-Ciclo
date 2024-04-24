using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    [SerializeField] CharacterJoint jointToCut;
    [SerializeField] GameObject box;
    [SerializeField] float force;
    Rigidbody rb;
    Collider localCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        localCollider = GetComponent<Collider>();
    }

    [ContextMenu("EXECUTECUT")]
    void ExecuteCut()
    {
        Vector3 dir = transform.forward;
        dir.y = 1;
        
        box.GetComponent<Rigidbody>().isKinematic = false;
        box.GetComponent<Rigidbody>().useGravity = true;
        box.transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        localCollider.isTrigger = false;
        jointToCut.connectedBody = rb;
        rb.velocity = dir * force;
    }
}
