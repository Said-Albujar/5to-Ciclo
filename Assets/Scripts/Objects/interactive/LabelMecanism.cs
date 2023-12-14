using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelMecanism : Mecanism
{
    public GameObject labelObject;
    public Renderer renderskin;
    public KeyCode KeyActiveLabel = KeyCode.E;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isBroken = false;
    [SerializeField] private bool canBroke = false;

    public ParticleSystem brokeVfx;
    public ParticleSystem fixVfx;

    public bool isNear;
    public bool labelActive;


    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        Changerenderskiner();
        LabelUse();


        if (isLocked || isBroken)
        {
            if (renderskin != null)
                renderskin.material.color = Color.green;
            
        }
        else if (!isLocked && !isBroken)
        {
            if (renderskin != null)
                renderskin.material.color = Color.yellow;
        }
    }

    public override void UseFunction(bool active)
    {
        if (scriptToActive.Count>0)
        {
            if (scriptToActive[0].GetComponent<InteractiveDoor>()) // Busca primero si el objeto a usar es una puerta
            {
                foreach (GameObject item in scriptToActive)
                {
                    item.GetComponent<InteractiveDoor>().openDoor = active;
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

    private void LabelUse()
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

    private void Changerenderskiner()
    {
        if (isLocked || isBroken)
        {
            if (renderskin != null)
            {
                renderskin.material.color = Color.red;
            }

        }
        else if (!isLocked && !isBroken)
        {
            if (renderskin != null)
            {
                renderskin.material.color = Color.green;
            }

        }
    }

    private void ReadInputs()
    {
        if (Input.GetKeyDown(KeyActiveLabel) && isNear && !isLocked && !isBroken)
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
        else if (Input.GetKeyDown(KeyActiveLabel) && isNear && isBroken)
        {
            if (brokeVfx != null)
                brokeVfx.Play();
            else
                Debug.Log("Referencia nula en brokeVfx");
            
        }

        if (changeCharacter != null)
        {
            if (changeCharacter.IsEngineer)
            {
                if (Input.GetKeyDown(KeyCode.F) && isLocked && !isBroken && isNear&&changeCharacter.HaveEngineer) // desbloquea el mecanismo si este no esta roto y se encuentra cerca
                {
                    isLocked = false;
                    labelActive = true;
                    AudioManager.Instance.PlaySFX("FixLabel");
                    if (fixVfx!=null)
                        fixVfx.Play();
                    else
                        Debug.Log("Referencia nula en fixVfx");

                }
              
                
                if (Input.GetKeyDown(KeyCode.G) && !isBroken && canBroke && isNear) //Si no esta roto y puede romperse, se rompera
                {
                    isBroken = true;
                    labelActive = true;
                    if (brokeVfx != null)
                        brokeVfx.Play();
                    else
                        Debug.Log("Referencia nula en brokeVfx");
                }
            }
            if (Input.GetKeyDown(KeyCode.F) && isLocked && !isBroken && isNear && !changeCharacter.HaveEngineer)
            {
                AudioManager.Instance.PlaySFX("Failed");
                Debug.Log("Ncecistas la lalve");
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
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi") || other.CompareTag("Mata"))
        {
            isNear = false;
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
