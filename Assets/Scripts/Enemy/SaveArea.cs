using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveArea : MonoBehaviour
{
    [SerializeField] CatEnemy catEnemy;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Hair")
        {
            catEnemy.canSeePlayer= false;
        }
    }
}
