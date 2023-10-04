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
    
    [Header("Button")]
    public Animator anim;
    public float timeActive;
    private float timer;    
    private bool timerActive;
    bool buttonPressed;

    [Header ("Function")]
    public List<GameObject> objectActive;

    public Renderer render;
    public ChangeCharacter changeMecanic;

    public bool isLocked = false;
    public bool isBroken = false;
    public bool canBroke = false;
    private void Awake()
    {
        changeMecanic = FindObjectOfType<ChangeCharacter>();
        render = GetComponent<Renderer>();
    }
    void Start()
    {
        if (typeSelect == Type.Button)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateTimerButton();
        LabelFunction();

        if (typeSelect == Type.Button)
            anim.SetBool("isPressed", buttonPressed);

        if (typeSelect == Type.Label && isNear && !isLocked && !isBroken)
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

        if (changeMecanic != null)
        {
            if (changeMecanic.IsEngineer)
            {
                if (Input.GetKeyDown(KeyCode.F) && isLocked && !isBroken && isNear) // desbloquea el mecanismo si este no esta roto y se encuentra cerca
                {
                    isLocked = false;
                }
                if (Input.GetKeyDown(KeyCode.G) && !isBroken && canBroke && isNear) //Si no esta roto y puede romperse, se rompera
                {
                    isBroken = true;
                }
            }
        }

        if (isLocked || isBroken)
        {
            if (render != null)
            {
                render.material.color = Color.red;
            }
                
        }
        else if(!isLocked && !isBroken)
        {
            if (render!=null)
            {
                render.material.color = Color.green;
            }
            
        }
    }

    private void LabelFunction()
    {
        if (typeSelect == Type.Label)
        {
            if (!isBroken)
            {
                switch (labelActive)
                {
                    case true:
                        RotateLabel(35);

                        UseFunction(true);

                        break;
                    case false:
                        RotateLabel(-35);

                        UseFunction(false);
                        break;
                }
            }
            else
            {
                UseFunction(false);
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
                UseFunction(false);

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

                UseFunction(true);

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

    private void UseFunction(bool active) //Activa o desactiva el mecanismo que se este utilizando
    {
            FunctionDoor(active);
            FunctionMovePlatform(active);
        
    }

    private void FunctionDoor(bool active)
    {
        if (objectActive[0].GetComponent<InteractiveDoor>()) // Busca primero si el objeto a usar es una puerta
        {
            foreach (GameObject item in objectActive)
            {
                item.GetComponent<InteractiveDoor>().open = active;
            }
        }
    }

    private void FunctionMovePlatform(bool active)
    {
        if (objectActive[0].GetComponent<MovingPlatform1>()) // Busca primero si el objeto a usar es una plataforma movible
        {
            foreach (GameObject item in objectActive)
            {
                item.GetComponent<MovingPlatform1>().enabled = active;
            }
        }
    }
}
