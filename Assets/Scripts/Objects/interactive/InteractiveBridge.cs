using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBridge : MonoBehaviour
{
    public Animator animator;
    public bool open;

    private void Update()
    {
        animator.SetBool("abiertoBridge", open);
    }
}
