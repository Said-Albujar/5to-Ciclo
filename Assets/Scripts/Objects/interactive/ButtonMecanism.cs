using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMecanism : Mecanism
{
    public Animator anim;
    public float timeActive;
    private float timer;
    private bool timerActive;
    public bool buttonPressed;
    public bool haveMoreButtons;
    public ButtonMecanism[] otherButtons;
    private bool oncePress;
    float timerSound;

    private float timerButtPress = 0.25f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (anim!=null)
        {
            anim.SetBool("isPressed", buttonPressed);
        }
        
        ActivateTimerButton();
        DesactButtonPressed();
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
        if (!timerActive && !buttonPressed)
        {
            UseFunction(false);
        }

        if (buttonPressed && !oncePress)
        {
            AudioManager.Instance.PlaySFX("ButtonOn");
            oncePress = true;
        }
        TimerSound();
    }
    public override void UseFunction(bool active)
    {
        if (scriptToActive.Count > 0 && scriptToActive[0] != null)
        {
            if (scriptToActive[0].GetComponent<InteractiveDoor>()) // Busca primero si el objeto a usar es una puerta
            {
                if (haveMoreButtons)
                {
                    foreach (ButtonMecanism button in otherButtons)
                    {
                        if (otherButtons.All(b => b.buttonPressed))
                        {
                            foreach (GameObject item in scriptToActive)
                            {
                                item.GetComponent<InteractiveDoor>().openDoor = active;
                            }
                        }
                        else
                        {
                            foreach (GameObject item in scriptToActive)
                            {
                                item.GetComponent<InteractiveDoor>().openDoor = false;
                            }
                        }
                    }
                }
                else
                {
                    foreach (GameObject item in scriptToActive)
                    {
                        item.GetComponent<InteractiveDoor>().openDoor = active;
                    }
                }
            }
            else if (scriptToActive[0].GetComponent<MovingPlatform1>()) // Busca primero si el objeto a usar es una plataforma movible
            {
                foreach (GameObject item in scriptToActive)
                {
                    item.GetComponent<MovingPlatform1>().enabled = active;
                }
            }
            else if (scriptToActive[0].GetComponent<Puente>())
            {
                foreach (GameObject item in scriptToActive)
                {
                    item.GetComponent<Puente>().openBridge = active;
                }
            }
            else
                Debug.LogError("No se adjunto un gameobject con script valido en scriptActive");
        }
    }

    private void TimerSound()
    {
        if (oncePress && !buttonPressed)
        {
            timerSound += Time.deltaTime;
            if (timer >= 0.25f)
            {
                oncePress = false;
                timerSound = 0f;
            }
        }
    }
    private void DesactButtonPressed()
    {
        timerButtPress -= Time.deltaTime;
        {
            if (timerButtPress <= 0)
            {
                buttonPressed = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            UseFunction(true);
            buttonPressed = true;
            timerButtPress= 0.25f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            buttonPressed = false;
            timer = timeActive;
            timerActive = true;
            oncePress = false;
            AudioManager.Instance.PlaySFX("ButtonOff");
        }
    }
}
