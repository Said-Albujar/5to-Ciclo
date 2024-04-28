using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [HideInInspector]public PlayerMovement playerMovement;
    public Animator anim;
    public PickUp picked; 
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.inPause)
        {
            anim.speed = 1;
            anim.SetBool("jumped", playerMovement.isJump);
            anim.SetBool("isRunning", playerMovement.isRunning);
            anim.SetBool("isCrouching", playerMovement.isCrouching);
            
            anim.SetBool("PickBox", picked.haveObject);
            anim.SetBool("jumpedBox", playerMovement.isJump);

            MoveAnim();
            JumpAnim();
            anim.SetBool("grounded", playerMovement.grounded);
            anim.SetBool("inBorder", playerMovement.currentstate == PlayerMovement.state.climbIdle);
        }

    }


    public void ClimbUpAnim()
    {
        anim.Play("ClimbUp");
        anim.SetBool("inBorder", false);
    }
    private void MoveAnim()
    {
        if (playerMovement.actualSpeed <= 0.1f)
        {
            anim.SetFloat("Move", 0f);
        }
        else
        {
            anim.SetFloat("Move", playerMovement.actualSpeed);
        }

    }


    private void JumpAnim()
    {
        if (playerMovement.grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !playerMovement.isCrouching)
            {
                anim.SetBool("jumped", true);
            }
            anim.SetBool("grounded", true);
        }

        else
        {
            Falling();
        }
    }

    private void Falling()
    {
        anim.SetBool("grounded", false);
        anim.SetBool("jumped", false);
    }

    public void StepSound()
    {
        //AudioManager.Instance.PlaySFX("Walk");
    }
}
