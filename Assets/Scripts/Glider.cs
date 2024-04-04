using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    public int impulse = 1000;
    public int tiempo = 10;

    public KeyCode flyKey = KeyCode.G;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Fly() //GLIDERRRR
    {
        if (Input.GetKeyDown(flyKey))
        {
            Debug.Log("i believe i can flyyy");
            rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
        }
    }

    void Update()
    {
        Fly();
    }
}
