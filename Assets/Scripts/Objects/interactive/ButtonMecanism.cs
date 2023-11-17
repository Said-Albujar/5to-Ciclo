using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMecanism : Mecanism
{
    public Animator anim;
    public float timeActive;
    private float timer;
    private bool timerActive;
    bool buttonPressed;
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
    }
    private void ActivateTimerButton()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                UseFunction(false,1);

                timerActive = false;
            }
        }
        if (!timerActive && !buttonPressed)
        {
            UseFunction(false,1);
        }

    }
    public override void UseFunction(bool active,float time)
    {
        if (scriptToActive.Count > 0 && scriptToActive[0] != null)
        {
            if (scriptToActive[0].GetComponent<InteractiveDoor>()) // Busca primero si el objeto a usar es una puerta
            {
                foreach (GameObject item in scriptToActive)
                {
                    item.GetComponent<InteractiveDoor>().openDoor = active;
                    StartCoroutine(Inactive(item, active,time));
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
                    StartCoroutine(Inactive(item, active, time));
                }
            }
            else
                Debug.LogError("No se adjunto un gameobject con script valido en scriptActive");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            UseFunction(true,0);
            buttonPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Box") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            buttonPressed = false;
            timer = timeActive;
            timerActive = true;
        }
    }
    IEnumerator Inactive(GameObject item, bool active, float time)
    {
        yield return new WaitForSeconds(time);
        if (scriptToActive[0].GetComponent<InteractiveDoor>())
        {
            //  yield return new WaitForSeconds(time);
            item.GetComponent<InteractiveDoor>().enabled = active;
        }
        if (scriptToActive[0].GetComponent<Puente>())
        {
            item.GetComponent<InteractiveDoor>().enabled = active;
        }

    }
}
