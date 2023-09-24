using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool IsPickable = true;
    public InteractivePalanca interactivePalanca;
    public string tagName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagName)
        {
            other.GetComponentInParent<Swing>().ObjectToPickup = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag == tagName)
        {

            other.GetComponentInParent<Swing>().ObjectToPickup = null;
        }
        
    }
    private void OnDestroy()
    {
        if(interactivePalanca!=null)
        interactivePalanca.islocked = false;
    }
}
