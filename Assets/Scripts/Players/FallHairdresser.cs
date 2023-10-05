using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHairdresser : MonoBehaviour
{
    public bool fall;
    public Rigidbody rb2d;
    public float fallPlayer;
    public PlayerMovement player;
    public float radius;
    public LayerMask wallMask;

    // Update is called once per frame
    void Update()
    {

       
        if (fall)
        {
            player.turn = false;
            rb2d.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            rb2d.drag = fallPlayer;
        }
            

        else
        {
           
            rb2d.drag = 0f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * radius);
    }

    private void OnCollisionStay(Collision collision)
    {
         if (collision.gameObject.CompareTag("Wall"))
         {
             fall = true;
         }
    }
    private void OnCollisionExit(Collision collision)
    {
         if (collision.gameObject.CompareTag("Wall"))
         {
             fall = false;
         }
    }
}
