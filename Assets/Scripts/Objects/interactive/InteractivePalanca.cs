using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InteractivePalanca : MonoBehaviour
{
    public bool islocked;
    public bool isMecanic;
    public bool isNear;
    public GameObject labelObject;
    public List<Puente> puente;
    public KeyCode keyActiveLabel;
    public bool labelActive;
    public ChangeCharacter changeMecanic;
    private void Update()
    {
        LabelFunction();
        if (Input.GetKeyDown(keyActiveLabel)&&!islocked&&isNear)
        {
            switch (labelActive)
            {
                case true:
                    labelActive = false;
                    break;
                case false:
                    labelActive = true;
                    break;
            }
        }
        if (changeMecanic.IsEngineer)
        {
            if (Input.GetKeyDown(KeyCode.E) &&islocked&&isNear)
            {
                islocked = false;

            }


        }
    }

    private void RotateLabel(float rotation)
    {
        Quaternion actualRotation = labelObject.transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotation, 0, 0);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, 200 * Time.deltaTime);

        labelObject.transform.rotation = actualRotation;
    }
    private void LabelFunction()
    {
        
        
            switch (labelActive)
            {
                case true:
                    RotateLabel(35);
                foreach (Puente item in puente)
                {
                    item.openBridge = true;
                }

                break;

                  

                    
                case false:
                    RotateLabel(-35);
                foreach (Puente item in puente)
                {
                    item.openBridge = false;
                }

                break;
            }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNear = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNear = false;
            

        }
    }
}
