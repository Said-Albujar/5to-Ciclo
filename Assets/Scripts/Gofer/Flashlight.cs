using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public  Light flashlight;
    public ChangeCharacter changeMecanic;
    public bool IsOn;

    // This method is called when the scene loads.
    private void Update()
    {
        if (changeMecanic.IsMiner == false)
        {
            flashlight.enabled = false;
        }

        if (changeMecanic.IsMiner && Input.GetKeyDown("q"))
        {
            AudioManager.Instance.PlaySFX("Lantern");
            if (flashlight.enabled == true)
            {
                IsOn = false;
                flashlight.enabled = false;
            }
            else if (flashlight.enabled == false)
            {
                IsOn = true;
                flashlight.enabled = true;
            }
        }
    }
}