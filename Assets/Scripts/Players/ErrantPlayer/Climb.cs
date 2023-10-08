using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEditor;

public class Climb : MonoBehaviour
{
    [Header("Climb")]
    public GameObject positionClimb;
    public Rigidbody rigidbodyPlayer;
    public GameObject colisionClimb;
    public float radius;
    public float elevator;
    [Header("ClimbWalls")]
    public bool enPared = false;
    public GameObject point;
    public float speed;
    public float time;

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
    

    IEnumerator ClimbAction()
    {

        if (enPared)
        {
            rigidbodyPlayer.useGravity = false;
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed * Time.deltaTime);
            yield return new WaitForSeconds(1f);



        }
        else
        {
            rigidbodyPlayer.useGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hold"))
        {
            enPared = true;
            StartCoroutine(ClimbAction());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hold"))
        {
            enPared = false;
            StartCoroutine(ClimbAction());
        }
    }
}
