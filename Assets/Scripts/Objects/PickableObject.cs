using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool IsPickable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractionZone")
        {
            other.GetComponentInParent<Swing>().ObjectToPickup = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "InteractionZone")
        {
            other.GetComponentInParent<Swing>().ObjectToPickup = null;
        }
    }
}
