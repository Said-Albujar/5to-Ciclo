using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [HideInInspector]public PlayerMovement playerMovement;
    public Animator anim;
    public AnimationClip[] usingToolsAnims; //0.alicate, 1.pico, 2.tijeras
    public AnimatorOverrideController animatorOverrideController;
    public PickUp picked;
    bool once = false;
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
            MoveAnimCrouch();
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
    private void MoveAnimCrouch()
    {
        if(playerMovement.isCrouching)
        {
            if (playerMovement.actualSpeed <= 0.1f)
            {
                anim.SetFloat("MoveCrouch", 0f);
            }
            else
            {
                anim.SetFloat("MoveCrouch", playerMovement.actualSpeed);
            }
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

    public void UsingToolAnim(int index)
    {
        anim.SetBool("usingTool", true);
        animatorOverrideController["UsingPickage"] = usingToolsAnims[index];
    }

    public void ExitToolAnim()
    {
        anim.SetBool("usingTool", false);
    }
}
