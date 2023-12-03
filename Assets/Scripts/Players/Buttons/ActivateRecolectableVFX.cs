using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRecolectableVFX : MonoBehaviour
{
    public GameObject buttonVfxPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstanstiateVFX()
    {
        Instantiate(buttonVfxPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair"))
        {
            InstanstiateVFX();
        }
    }
}
