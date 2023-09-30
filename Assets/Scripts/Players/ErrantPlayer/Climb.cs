using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [Header("Climb")]
    public GameObject positionClimb;
    public Rigidbody rigidbodyPlayer;
    public GameObject colisionClimb;
    public float radius;
    public float elevator;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        climbPlayer();
    }
    void climbPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(positionClimb.transform.position,positionClimb.transform.TransformDirection(Vector3.forward),out hit,radius))
        {
            if(hit.collider.gameObject.CompareTag("climb"))
            {
                rigidbodyPlayer.transform.position += new Vector3(0f, elevator, 0f);
                rigidbodyPlayer.useGravity = false;

            }
            
             
           
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(positionClimb.transform.position, positionClimb.transform.TransformDirection(Vector3.forward) * radius);
    }
}
