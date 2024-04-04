using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatArea : MonoBehaviour
{
    public Action PlayerInTheArea;
    public Action PlayerOutTheArea;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            PlayerInTheArea?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            PlayerOutTheArea?.Invoke();
        }
    }
}
