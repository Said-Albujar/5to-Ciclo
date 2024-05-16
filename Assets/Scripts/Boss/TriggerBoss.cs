using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public Boss boss;
    public int indexPhase;
    public int indexPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair"))
        {
            boss.active = true;
            boss.actualPhase = indexPhase;
            boss.indexPoint = indexPoint;
        }
        
    }
}
