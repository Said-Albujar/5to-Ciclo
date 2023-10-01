using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    public bool openBridge;
    public float rotationOpen;
    public float rotationClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(openBridge)
        {
            RotateLabel(rotationOpen);
            /*if(transform.rotation.z!=0)
            {
                
            }*/

        }
        else
        {
            RotateLabel(rotationClose);
        }
    }
    private void RotateLabel(float rotation)
    {
        Quaternion actualRotation = transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotation, 0, 0);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, 100 * Time.deltaTime);

        transform.rotation = actualRotation;
    }
}
