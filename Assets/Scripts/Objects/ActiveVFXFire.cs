using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ActiveVFXFire : MonoBehaviour
{
    public GameObject vfx;
    public float distance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
        if ( CalculateDistance() < distance)
        {
            if (!vfx.activeSelf)
            {
                vfx.SetActive(true);
            }
        }
        else
        {
            if (vfx.activeSelf)
            {
                vfx.SetActive(false);
            }
        }
    }
    float CalculateDistance()
    {
        return Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
    }
}
