using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    PlayerMovement pm;

    [Header("Border Detection")]
    public LayerMask solidMask;
    public Transform raycastCenterStart;
    public Transform raycastUpStart;
    [SerializeField] bool inBorder;

    [Header("Border Action")]
    public float borderCooldown;
    float timer;
    float verticalInput;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        verticalInput = pm.GetPlayerInput().y;

        if(timer > 0) timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(timer <= 0f && !inBorder) DetectBorder();

        if (inBorder) BorderAction();
    }

    private void DetectBorder()
    {
        inBorder = Physics.Raycast(raycastCenterStart.position, raycastCenterStart.forward, .1f, solidMask, QueryTriggerInteraction.Ignore) && !Physics.Raycast(raycastUpStart.position, raycastUpStart.forward, .1f, solidMask, QueryTriggerInteraction.Ignore);

        if(inBorder)
        {
            pm.SetFreeze(true);
        }
    }

    private void BorderAction()
    {
        if (verticalInput == 0f) return;

        inBorder = false;
        timer = borderCooldown;
        pm.SetFreeze(false);

        if (verticalInput > 0f)
        {
            pm.TriggerJump();
        }
    }
}
