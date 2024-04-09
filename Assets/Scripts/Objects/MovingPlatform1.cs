using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public float waitTime = 1.0f; 
    private Vector3 target;
    public bool isWaiting = false;

    void Start()
    {
        target = pointB.position;
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
            {
                target = pointB.position;
                StartCoroutine(WaitForNextMove());
            }
            else if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
            {
                target = pointA.position;
                StartCoroutine(WaitForNextMove());
            }
        }
    }

    IEnumerator WaitForNextMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
