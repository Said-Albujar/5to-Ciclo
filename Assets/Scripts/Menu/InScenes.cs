using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InScenes : MonoBehaviour
{
    private void Awake()
    {
        var dontDestroyInScenes = FindObjectsOfType<InScenes>();
        if (dontDestroyInScenes.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }
}
