using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    public bool openBridge;
    public float rotationOpenX,rotationOpenY,rotationOpenZ;
    public float rotationCloseX,rotationCloseY,rotationCloseZ;
    public float speedRotation;
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
            /*if(transform.rotation.z!=0)
            {
                
            }*/

        }
        else
        {
            RotateLabel(rotationCloseX,rotationCloseY,rotationCloseZ);
        }
    }
    private void RotateLabel(float rotationX,float rotationY,float rotationZ)
    {
        Quaternion actualRotation = transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotationX, rotationY, rotationZ);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, speedRotation * Time.deltaTime);

        transform.rotation = actualRotation;
    }
}
