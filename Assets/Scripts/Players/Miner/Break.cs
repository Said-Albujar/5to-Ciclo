using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public bool CanBreak = false;
    public ChangeCharacter Change;
    public GameObject Rock;



    void Update()
    {
        if (CanBreak == true && Input.GetKeyDown(KeyCode.F) && Change.IsMiner == true)
        {
            Destroy(Rock);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Miner"))
        {
            CanBreak = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Miner"))
        {
            CanBreak = false;
        }
    }
}
