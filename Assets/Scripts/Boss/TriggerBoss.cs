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
        boss.active = true;
        boss.actualPhase = indexPhase;
        boss.indexPoint = indexPoint;
    }
}
