using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InteractivePalanca : MonoBehaviour
{
    public bool isLocked;
    public bool isNear;
    public GameObject labelObject;
    public List<Puente> puente;
    public KeyCode keyActiveLabel;
    public bool labelActive;
    public ChangeCharacter changeMecanic;
    public Renderer render;

    public bool isBroken = false;
    public bool canBroke = false;

    private void Awake()
    {
        changeMecanic = FindObjectOfType<ChangeCharacter>();
    }
    private void Update()
    {
        LabelFunction();
        if (Input.GetKeyDown(keyActiveLabel) && !isLocked && isNear && !isBroken)
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
        if(changeMecanic!=null)
        {
            if (changeMecanic.IsEngineer)
            {
                if (Input.GetKeyDown(KeyCode.F) && isLocked && !isBroken && isNear)
                {
                    isLocked = false;

                }
                if (Input.GetKeyDown(KeyCode.G) && !isBroken && canBroke && isNear) //Si no esta roto y puede romperse, se rompera
                {
                    isBroken = true;
                }

            }
        }

        if (isLocked || isBroken)
        {
            render.material.color = Color.red;
        }
        else if (!isLocked && !isBroken)
        {
            render.material.color = Color.green;
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
        if (!isBroken)
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
        else
        {
            foreach (Puente item in puente)
            {
                item.openBridge = false;
            }
        }
            
        
           
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Hair") || collision.CompareTag("Miner") || collision.CompareTag("Engi"))
        {
            isNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi"))
        {
            isNear = false;
        }
    }
}
