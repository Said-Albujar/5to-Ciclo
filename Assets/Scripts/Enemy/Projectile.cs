using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Update()
    {
        Destroy(gameObject, 3f);
    }

}
