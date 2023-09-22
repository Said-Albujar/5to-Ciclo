using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public bool CanBreak = false;
    public string TagObj = "Player";
    public Renderer Miner;


    void Update()
    {
        if (CanBreak && Input.GetKeyDown(KeyCode.F) && Miner.material.color == Color.blue)
        {
            BreakMiner();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagObj))
        {
            CanBreak = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagObj))
        {
            CanBreak = false;
        }
    }

    private void BreakMiner()
    {
        Renderer ObjRenderer = GetComponent<Renderer>();
        if (ObjRenderer != null)
        {
            Destroy(gameObject);
        }
    }
   
}
