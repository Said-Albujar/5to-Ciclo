using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    public bool openDoor;
    public float speedRotation;
    [Header("Rotations")]
    public float rotationOpenX;
    public float rotationOpenY;
    public float rotationOpenZ;
    private bool wasOpen=false;
    public float rotationCloseX, rotationCloseY, rotationCloseZ;
    public float time;
    void Update()
    {
        if (openDoor)
        {
            Rotate(rotationOpenX, rotationOpenY, rotationOpenZ);
            if(!wasOpen)
            {
                AudioManager.Instance.PlaySFX("abrir");

            }
        }
        else
        {
            Rotate(rotationCloseX, rotationCloseY, rotationCloseZ);
            if (wasOpen)
            {
                AudioManager.Instance.PlaySFX("cerrar");

            }
        }
        wasOpen = openDoor;
    }
    private void Rotate(float rotationX, float rotationY, float rotationZ)
    {
        Quaternion actualRotation = transform.rotation;
        Quaternion rotationActive = Quaternion.Euler(rotationX, rotationY, rotationZ);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, speedRotation * Time.deltaTime);
        //actualRotation = Quaternion.Lerp(actualRotation, rotationActive, time);
        transform.rotation = actualRotation;
    }
}
