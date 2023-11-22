using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    public bool openBridge;
    public float speedRotation;
    [Header("Rotations")]
    public float rotationOpenX;
    public float rotationOpenY;
    public float rotationOpenZ;
    private bool wasOpen;
    public float rotationCloseX,rotationCloseY,rotationCloseZ;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(openBridge)
        {
            RotateLabel(rotationOpenX,rotationOpenY,rotationOpenZ);
            if (!wasOpen)
            {
                AudioManager.Instance.PlaySFX("abrirPuente");

            }

        }
        else
        {
            RotateLabel(rotationCloseX,rotationCloseY,rotationCloseZ);
            if (wasOpen)
            {
                AudioManager.Instance.PlaySFX("cerrar");

            }
        }
        wasOpen = openBridge;
    }
    private void RotateLabel(float rotationX,float rotationY,float rotationZ)
    {
        Quaternion actualRotation = transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotationX, rotationY, rotationZ);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, speedRotation * Time.deltaTime);

        transform.rotation = actualRotation;
    }
}
