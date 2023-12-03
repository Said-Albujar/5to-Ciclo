using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float RotationSpeed;


    public bool rotation = true;


    void Update()
    {
        if (rotation == true)
        {
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }
    }

}
