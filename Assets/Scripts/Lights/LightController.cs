using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] GameObject[] light1;
    [SerializeField] GameObject[] light2;

    [SerializeField] bool x;
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Hair")
        {
            if(x)
            {
                if(other.transform.position.x > transform.position.x)
                {
                    if(light1 != null)
                    foreach (var item in light1)
                    {
                        if(item != null)
                        item.SetActive(false);
                    }
                    

                    if(light2 != null)
                    foreach (var item in light2)
                    {
                        if(item != null)
                        item.SetActive(true);
                    }
                }
                else
                {
                    if(light1 != null)
                    foreach (var item in light1)
                    {
                        if(item != null)
                        item.SetActive(true);
                    }
                    

                    if(light2 != null)
                    foreach (var item in light2)
                    {
                        if(item != null)
                        item.SetActive(false);
                    }
                }
            }
            else
            {
                if(other.transform.position.z > transform.position.z)
                {
                    if(light1 != null)
                    foreach (var item in light1)
                    {
                        if(item != null)
                        item.SetActive(false);
                    }
                    

                    if(light2 != null)
                    foreach (var item in light2)
                    {
                        if(item != null)
                        item.SetActive(true);
                    }
                }
                else
                {
                    if(light1 != null)
                    foreach (var item in light1)
                    {
                        if(item != null)
                        item.SetActive(true);
                    }
                    

                    if(light2 != null)
                    foreach (var item in light2)
                    {
                        if(item != null)
                        item.SetActive(false);
                    }
                }
            }
        }
    }
}
