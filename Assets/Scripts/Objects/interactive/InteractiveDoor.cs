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

    public float rotationCloseX, rotationCloseY, rotationCloseZ;
    void Update()
    {
        if (openDoor)
        {
            Rotate(rotationOpenX, rotationOpenY, rotationOpenZ);
        }
        else
        {
            Rotate(rotationCloseX, rotationCloseY, rotationCloseZ);
        }
    }
    private void Rotate(float rotationX, float rotationY, float rotationZ)
    {
        Quaternion actualRotation = transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotationX, rotationY, rotationZ);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, speedRotation * Time.deltaTime);

        transform.rotation = actualRotation;
    }
}
