using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public PlayerLight lightActive;
    public GameObject flashLight;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (lightActive.enabled)
            {
                lightActive.enabled = false;
                flashLight.SetActive(false);

            }
            else
            {
                lightActive.enabled = true;
                flashLight.SetActive(true);

            }

        }
    }
}