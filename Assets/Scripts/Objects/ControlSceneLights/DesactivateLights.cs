using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateLights : ControlSceneLightings
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DesactivateLighting()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair"))
        {
            DesactivateLighting();
        }
        
    }

}
