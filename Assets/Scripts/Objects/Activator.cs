using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject interactableCanvas; 
    public GameObject objectToDeactivate; 
    private bool isInsideTrigger = false;
    private bool isCanvasActive = false;
    private bool isGamePaused = false;

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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(true);
                }
            }
            else
            {
                interactableCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;              
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(false);
                }
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
            Time.timeScale = 1f;
        }
    }
}
