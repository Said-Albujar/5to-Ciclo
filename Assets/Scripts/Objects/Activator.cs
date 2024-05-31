using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Activator : MonoBehaviour
{
    Animator animator;
    PlayerAnimationController anim;
    PlayerMovement movement;
    PlayerHealth health;
    public InteractiveDoor interactiveDoor;
    public GameObject interactableCanvas;
    public GameObject objectToDeactivate;
    public CinemachineBrain cinemachineBrain;
    private bool isInsideTrigger = false;
    private bool isCanvasActive = false;
    private bool isGamePaused = false;
    private bool isCameraLocked = false;
    static public bool codeIsActive;

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        anim = GetComponent<PlayerAnimationController>();
        movement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isCanvasActive)
            {
                health.uiActive = false;
                animator.enabled = true;
                anim.enabled = true;
                movement.enabled = true;
                interactableCanvas.SetActive(false);
                codeIsActive = false;
                isCameraLocked = true;
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(true);
                    if (interactiveDoor != null)
                    {
                        objectToDeactivate.SetActive(false);
                    }
                }
            }
            else
            {
                health.uiActive = true;
                animator.enabled = false;
                anim.enabled = false;
                movement.enabled = false;
                interactableCanvas.SetActive(true);
                codeIsActive = true;
                isCameraLocked = false;
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(false);
                }
            }

            if (cinemachineBrain != null)
            {
                cinemachineBrain.enabled = isCameraLocked;
            }

            if (!isCanvasActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }

            isCanvasActive = !isCanvasActive;
            isGamePaused = !isGamePaused;
            //Time.timeScale = isGamePaused ? 0f : 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactuable"))
        {
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactuable"))
        {
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        health.uiActive = false;
        animator.enabled = true;
        objectToDeactivate.SetActive(true);
        codeIsActive = false;
        anim.enabled = true;
        isCameraLocked = true;
        movement.enabled = true;
        isInsideTrigger = false;
        interactableCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCanvasActive = false;
        isGamePaused = false;
        Time.timeScale = 1f;
        cinemachineBrain.enabled = true;
    }

    public void BackButton()
    {
        health.uiActive = false;
        animator.enabled = true;
        objectToDeactivate.SetActive(true);
        codeIsActive = false;
        anim.enabled = true;
        isCameraLocked = true;
        movement.enabled = true;
        interactableCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCanvasActive = false;
        isGamePaused = false;
        Time.timeScale = 1f;
        cinemachineBrain.enabled = true;
    }
}
