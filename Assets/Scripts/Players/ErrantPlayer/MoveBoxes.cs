using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    PlayerMovement pm;
    Rigidbody rb;

    [Header("Input")]
    public KeyCode moveBoxKey;
    bool moveBox;

    [Header("Box Detection")]
    public LayerMask solidMask;
    public string boxTag;
    public float handForwardDistance;
    public Transform handPosition;
    private GameObject boxObj;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        GetBox();

        MoveBox();
    }

    private void GetInput()
    {
        if(Input.GetKeyDown(moveBoxKey))
        {

            moveBox = true;
        } else if(Input.GetKeyUp(moveBoxKey))
        { 
            moveBox = false;
        }
    }

    private void GetBox()
    {
        if (moveBox)
        {
            RaycastHit boxHit;

            if(Physics.Raycast(handPosition.position, handPosition.forward, out boxHit, handForwardDistance, solidMask))
            {
                if(boxHit.collider.gameObject.CompareTag(boxTag))
                {

                    AudioManager.Instance.PlaySFX("mover");

                    boxObj = boxHit.collider.gameObject;
                }
            }
        }
        else boxObj = null;
    }

    private void MoveBox()
    {
        if (boxObj == null || !moveBox) return;

        boxObj.GetComponent<Rigidbody>().velocity = rb.velocity;
    }
}
