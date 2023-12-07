using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLights : ControlSceneLightings
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateLighting()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair"))
        {
            ActivateLighting();
        }

    }
}
