using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public UnityEngine. Light flashlight;

    // This method is called when the scene loads.
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (flashlight.enabled == true)
            {
                flashlight.enabled = false;
            }
            else if (flashlight.enabled == false)
            {
                flashlight.enabled = true;
            }    
        }    
    }


}