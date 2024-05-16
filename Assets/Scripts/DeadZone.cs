using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Boss boss;


    private void OnCollisionEnter(Collision collision)
    {
        boss.playerInGround = true;
    }
}
