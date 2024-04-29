using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCharacter : MonoBehaviour, IDataPersistence
{
    public bool CanChange = true;
    public float TimeLeft = 0f;
    public float WaitTime = 5f;
    public PickUp pickUp;
    [Header("Hairdresser")]
    public GameObject HairdresserSkin;
    public GameObject HairdresserBody;
    public bool isNearH;
    public bool IsHairdress;
    public bool HaveHairdress;

    [Header("Miner")]
    public GameObject MinerSkin;
    public GameObject helmetSkin;
    public GameObject MinerBody;
    public bool isNearM;
    public bool IsMiner;
    public bool HaveMiner;

    [Header("Engineer")]
    public GameObject EngineerSkin;
    public GameObject EngineerBody;
    public bool isNearE;
    public bool IsEngineer;
    public bool HaveEngineer;

    [Header("CharacterChangeUI")]
    public GameObject characterHair;
    public GameObject characterMiner;
    public GameObject characterEngineer;
    public GameObject VFXchangeCharacter;
    public Vector3 positionVFX;
    public Quaternion rotationVFX;
    public bool execute;
    public bool alpha1Pressed = false;
    public bool alpha2Pressed = false;
    public bool alpha3Pressed = false;

    [HideInInspector]public PlayerAnimationController anim;

    void Start()
    {
        anim = GetComponent<PlayerAnimationController>();
        /* ChangeToHair();
         characterHair.SetActive(true);*/
    }

    void Update()
    {
        
        if(pickUp.haveObject)
        {
            StopChange();
           
        }
        else
        {
            if (IsHairdress)
            {
                HairdresserSkin.SetActive(true);
                MinerSkin.SetActive(false);
                EngineerSkin.SetActive(false);
            }
            if (IsEngineer)
            {
                HairdresserSkin.SetActive(false);
                MinerSkin.SetActive(false);
                EngineerSkin.SetActive(true);
            }
            if (IsMiner)
            {
                HairdresserSkin.SetActive(false);
                MinerSkin.SetActive(true);
                EngineerSkin.SetActive(false);
            }
        }
        Vector3 offset = new Vector3(0F, 1f, 0f);
        if (CanChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && HaveHairdress && !execute && !alpha1Pressed)
            {
                alpha1Pressed = true; // Marcar que Alpha1 ha sido presionada.
                alpha2Pressed = false;
                alpha3Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX + offset, transform.rotation * rotationVFX);
                obj.transform.SetParent(transform);
                Destroy(obj, 1.4f);
                ChangeToHair();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && HaveMiner && !execute && !alpha2Pressed)
            {
                alpha2Pressed = true; // Marcar que Alpha2 ha sido presionada.
                alpha1Pressed = false;
                alpha3Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX + offset, transform.rotation * rotationVFX);
                obj.transform.SetParent(transform);
                Destroy(obj, 1.4f);
                ChangeToMiner();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && HaveEngineer && !execute && !alpha3Pressed)
            {
                alpha3Pressed = true; // Marcar que Alpha3 ha sido presionada.
                alpha2Pressed = false;
                alpha1Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX + offset, transform.rotation * rotationVFX);
                obj.transform.SetParent(transform);
                Destroy(obj, 1.4f);
                ChangeToEngineer();
            }
        }
        else
        {
            TimeLeft -= Time.deltaTime;

            if (TimeLeft <= 0f)
            {
                CanChange = true;

            }
        }

        if (isNearM && Input.GetKeyDown(KeyCode.F))
        {
            
            isNearM = false;
            HaveMiner = true;
            ChangeToMiner();
            Destroy(MinerBody);
            DataPersistenceManager.instance.SaveGame();
        }

        if (isNearE && Input.GetKeyDown(KeyCode.F))
        {
            isNearE = false;
            HaveEngineer = true;
            ChangeToEngineer();
            Destroy(EngineerBody);
            DataPersistenceManager.instance.SaveGame();

        }
        if (isNearH && Input.GetKeyDown(KeyCode.F))
        {
            isNearH = false;
            HaveHairdress = true;
            ChangeToHair();
            Destroy(HairdresserBody);
            DataPersistenceManager.instance.SaveGame();

        }

        if (HaveMiner)
        {
            characterMiner.SetActive(true);
        }

        if (HaveEngineer)
        {
            characterEngineer.SetActive(true);
        }

        if (HaveHairdress)
        {
            characterHair.SetActive(true);
        }
    }

    void ChangeToHair()
    {
        StartTimer();

        HairdresserSkin.SetActive(true);
        MinerSkin.SetActive(false);
        helmetSkin.SetActive(false);
        EngineerSkin.SetActive(false);

        IsHairdress = true;
        IsMiner = false;
        IsEngineer = false;
    }

    void ChangeToMiner()
    {
        StartTimer();

        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(true);
        helmetSkin.SetActive(true);

        EngineerSkin.SetActive(false);

        IsHairdress = false;
        IsMiner = true;
        IsEngineer = false;
    }
    void ChangeToEngineer()
    {
        StartTimer();

        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(false);
        helmetSkin.SetActive(false);
        EngineerSkin.SetActive(true);

        IsHairdress = false;
        IsMiner = false;
        IsEngineer = true;
    }
    void StopChange()
    {
        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(false);
        EngineerSkin.SetActive(false);
    }

    void StartTimer()
    {
        TimeLeft = WaitTime;
        CanChange = false;
        /*GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX, transform.rotation * rotationVFX);
        obj.transform.SetParent(transform);*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("B_Miner"))
        {
            isNearM = true;
        }

        if (other.CompareTag("B_Engi"))
        {
            isNearE = true;
        }

        if (other.CompareTag("B_Hair"))
        {
            isNearH = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("B_Miner"))
        {
            isNearM = false;
        }

        if (other.CompareTag("B_Engi"))
        {
            isNearE = false;
        }

        if (other.CompareTag("B_Hair"))
        {
            isNearH = false;
        }
    }

    public void LoadData(GameData data)
    {
        this.HaveMiner = data.haveMiner;
        this.HaveEngineer = data.haveEngineer;
        this.HaveHairdress = data.haveHairdress;
    }

    public void SaveData(ref GameData data)
    {
        data.haveMiner = this.HaveMiner;
        data.haveEngineer = this.HaveEngineer;
        data.haveHairdress = this.HaveHairdress;
    }
}
