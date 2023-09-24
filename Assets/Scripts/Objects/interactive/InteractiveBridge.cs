using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBridge : MonoBehaviour
{
   

    private void Update()
    {
       
       
    }

    public void RotateBridgeOpen(float rotation)
    {
        Quaternion actualRotation =transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotation, 0, 0);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, 200 * Time.deltaTime);

        transform.rotation = actualRotation;
    }
    public void RotateBridgeClose(float rotation)
    {
        Quaternion actualRotation = transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotation, 0, 0);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, 200 * Time.deltaTime);

        transform.rotation = actualRotation;
    }
}
