using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public enum Type
    {
        Button,
        Label
    }
    public Type typeSelect;

    [Header("Label")]
    public GameObject labelObject;
    public KeyCode KeyActiveLabel = KeyCode.E;
    private bool isNear;
    private bool labelActive;
    public bool isLocked = false;
    
    [Header("Button")]
    public Animator anim;
    public float timeActive;
    private float timer;
    private bool timerActive;
    bool buttonPressed;

    [Header ("Function")]
    public GameObject objectActive;
   
    



    // Start is called before the first frame update
    void Start()
    {
        if (typeSelect == Type.Button)
        {
            anim = GetComponent<Animator>();
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivateTimerButton();
        LabelFunction();
        if (typeSelect == Type.Button)
        {
            anim.SetBool("isPressed", buttonPressed);
        }

        if (typeSelect == Type.Label && isNear && !isLocked)
        {
            if (Input.GetKeyDown(KeyActiveLabel))
            {
                switch (labelActive)
                {
                    case true:
                        labelActive = false;
                        break;
                    case false:
                        labelActive = true;
                        break;
                }
            }
        }

    }

    private void LabelFunction()
    {
        if (typeSelect == Type.Label)
        {
            switch (labelActive)
            {
                case true:
                    RotateLabel(35);
                    if (objectActive.GetComponent<InteractiveBridge>())
                    {
                        objectActive.GetComponent<InteractiveBridge>().open = true;
                    }
                   
                    else
                        objectActive.SetActive(true);

                    break;
                case false:
                    RotateLabel(-35);
                    if (objectActive.GetComponent<InteractiveBridge>())
                    {
                        objectActive.GetComponent<InteractiveBridge>().open = false;
                    }
            
                    else
                        objectActive.SetActive(false);
                    break;
            }
        }
    }
    private void ActivateTimerButton()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (objectActive.GetComponent<InteractiveBridge>())
                {
                    objectActive.GetComponent<InteractiveBridge>().open = false;

                }
                else
                    objectActive.SetActive(false);
                    
                timerActive = false;
            }
        }
    }

    private void RotateLabel(float rotation)
    {
        Quaternion actualRotation = labelObject.transform.rotation;

        Quaternion rotationActive = Quaternion.Euler(rotation, 0, 0);

        actualRotation = Quaternion.RotateTowards(actualRotation, rotationActive, 200 * Time.deltaTime);

        labelObject.transform.rotation = actualRotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (typeSelect == Type.Button)
        {
            if (other.CompareTag("Player") || other.CompareTag("Box"))
            {

                if (objectActive.GetComponent<InteractiveBridge>())
                    objectActive.GetComponent<InteractiveBridge>().open = true;
                else
                {
                    if (!objectActive.activeSelf)
                    {
                        objectActive.SetActive(true);
                    }
                }
                
                buttonPressed = true;
            }

            
        }

        if (typeSelect == Type.Label)
        {
            if (other.CompareTag("Player"))
            {
                isNear = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (typeSelect == Type.Button)
        {
            if (other.CompareTag("Player") || other.CompareTag("Box"))
            {
                buttonPressed = false;
                timer = timeActive;
                timerActive = true;
            }
        }

        if (typeSelect == Type.Label)
        {
            if (other.CompareTag("Player"))
            {
                isNear = false;
            }
        }
    }
}
