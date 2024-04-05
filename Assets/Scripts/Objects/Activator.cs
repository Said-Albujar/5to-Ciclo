using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Activator : MonoBehaviour
{
    public InteractiveDoor interactiveDoor;
    public GameObject interactableCanvas;
    public GameObject objectToDeactivate;
    public CinemachineBrain cinemachineBrain;
    private bool isInsideTrigger = false;
    private bool isCanvasActive = false;
    private bool isGamePaused = false;
    private bool isCameraLocked = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isCanvasActive)
            {
                interactableCanvas.SetActive(false);
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
                interactableCanvas.SetActive(true);
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
            Time.timeScale = isGamePaused ? 0f : 0.5f;
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
            isInsideTrigger = false;
            interactableCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isCanvasActive = false;
            isGamePaused = false;
            isCameraLocked = false;
            Time.timeScale = 1f;
        }
    }
}
