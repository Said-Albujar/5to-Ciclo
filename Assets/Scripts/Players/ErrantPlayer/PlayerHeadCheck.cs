using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCheck : MonoBehaviour
{
    public float height;
    public static bool headCheck;
    private void Update()
    {
        HeadCheck();
    }
    void HeadCheck()
    {
        headCheck = Physics.Raycast(transform.position, Vector3.up, height);
    }
}
