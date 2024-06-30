using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDown : MonoBehaviour
{
    public MovingPlatform1 movingPlatform;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hair"))
        {
            bool coll = collision.transform.position.y < transform.position.y;
            if (coll)
            {
                movingPlatform.up = true;
                movingPlatform.down = false;
                movingPlatform.isWaiting = true;
                movingPlatform.StartCoroutine(movingPlatform.WaitForNextMove());


            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("TargetPlayer"))
        {
            movingPlatform.distance = 0.01f;
        }
    }
}