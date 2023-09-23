using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBridge : MonoBehaviour
{
    public bool keyActive;
    public GameObject bridge;
    public bool bridgeExist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Destroy(bridge);
                bridgeExist = true;
            }
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            keyActive = true;
            
           
        }
    }
}
