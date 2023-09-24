using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public  Light flashlight;
    public ChangeCharacter changeMecanic;

    // This method is called when the scene loads.
    private void Update()
    {
        if (changeMecanic.IsMiner == false)
        {
            flashlight.enabled = false;
        }

        if (changeMecanic.IsMiner && Input.GetKeyDown("q"))
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