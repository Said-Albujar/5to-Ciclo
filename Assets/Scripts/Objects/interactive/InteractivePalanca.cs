using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePalanca : MonoBehaviour
{
    public GameObject labelObject;
    public GameObject objectActive;
    public bool labelActive;
    public Type typeSelect;
    public enum Type
    {
        Label,
    }
    private void Update()
    {
        LabelFunction();
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
        if (typeSelect == Type.Label)
        {
            switch (labelActive)
            {
                case true:
                    RotateLabel(35);
                    if (objectActive.GetComponent<InteractiveBridge>())
                    {
                        objectActive.GetComponent<InteractiveBridge>().open = true;
                    }

                    else
                        objectActive.SetActive(true);

                    break;
                case false:
                    RotateLabel(-35);
                    if (objectActive.GetComponent<InteractiveBridge>())
                    {
                        objectActive.GetComponent<InteractiveBridge>().open = false;
                    }

                    else
                        objectActive.SetActive(false);
                    break;
            }
        }
    }
}
