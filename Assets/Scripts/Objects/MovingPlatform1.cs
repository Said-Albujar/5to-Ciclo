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
    public float distance = 0.01f;
    public bool up;
    public bool down = true;

    void Start()
    {
        target = pointB.position;
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            if (down)
            {
                MovePlatform(pointB.position);

                if (Vector3.Distance(transform.position, pointB.position) < distance)
                {
                    up = true;
                    down = false;
                    StartCoroutine(WaitForNextMove());
                }
            }
            else if (up)
            {
                MovePlatform(pointA.position);

                if (Vector3.Distance(transform.position, pointA.position) < distance)
                {
                    down = true;
                    up = false;
                    StartCoroutine(WaitForNextMove());
                }
            }
        }
    }

    void MovePlatform(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public IEnumerator WaitForNextMove()
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