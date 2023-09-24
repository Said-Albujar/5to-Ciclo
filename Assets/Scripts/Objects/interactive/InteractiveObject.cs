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
    public List<GameObject> objectActive;
   
    



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
                    if (objectActive[0].GetComponent<InteractiveDoor>())
                    {
                        foreach (GameObject item in objectActive)
                        {
                            item.GetComponent<InteractiveDoor>().open = true;
                        } 
                    }

                    if (objectActive[0].GetComponent<MovingPlatform1>())
                    {
                        foreach (GameObject item in objectActive)
                        {
                            item.GetComponent<MovingPlatform1>().enabled = true;
                        }    
                    }

                    break;
                case false:
                    RotateLabel(-35);
                    if (objectActive[0].GetComponent<InteractiveDoor>())
                    {
                        foreach (GameObject item in objectActive)
                        {
                            item.GetComponent<InteractiveDoor>().open = false;
                        }
                    }

                    if (objectActive[0].GetComponent<MovingPlatform1>())
                    {
                        foreach (GameObject item in objectActive)
                        {
                            item.GetComponent<MovingPlatform1>().enabled = false;
                        }
                    }
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
                if (objectActive[0].GetComponent<InteractiveDoor>())
                {
                    foreach (GameObject item in objectActive)
                    {
                        item.GetComponent<InteractiveDoor>().open = false;
                    }

                }

                if (objectActive[0].GetComponent<MovingPlatform1>())
                {
                    foreach (GameObject item in objectActive)
                    {
                        item.GetComponent<MovingPlatform1>().enabled = false;
                    }
                }

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
            if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi"))
            {

                if (objectActive[0].GetComponent<InteractiveDoor>())
                {
                    foreach (GameObject item in objectActive)
                    {
                        item.GetComponent<InteractiveDoor>().open = true;
                    }
                }

                if (objectActive[0].GetComponent<MovingPlatform1>())
                {
                    foreach (GameObject item in objectActive)
                    {
                        item.GetComponent<MovingPlatform1>().enabled = true;
                    }
                }
                
                buttonPressed = true;
            }

            
        }

        if (typeSelect == Type.Label)
        {
            if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi"))
            {
                isNear = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (typeSelect == Type.Button)
        {
            if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi"))
            {
                buttonPressed = false;
                timer = timeActive;
                timerActive = true;
            }
        }

        if (typeSelect == Type.Label)
        {
            if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi"))
            {
                isNear = false;
            }
        }
    }
}
