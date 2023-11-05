using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCharacter : MonoBehaviour, IDataPersistence
{
    public bool CanChange = true;
    public float TimeLeft = 0f;
    public float WaitTime = 5f;

    [Header("Hairdresser")]
    public GameObject HairdresserSkin;
    public GameObject HairdresserBody;
    public bool isNearH;
    public bool IsHairdress;
    public bool HaveHairdress;

    [Header("Miner")]
    public GameObject MinerSkin;
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

    void Start()
    {
        /* ChangeToHair();
         characterHair.SetActive(true);*/
    }

    void Update()
    {

        if (CanChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && HaveHairdress && !execute && !alpha1Pressed)
            {
                alpha1Pressed = true; // Marcar que Alpha1 ha sido presionada.
                alpha2Pressed = false;
                alpha3Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX, transform.rotation * rotationVFX);
                obj.transform.SetParent(transform);
                Destroy(obj, 1.4f);
                ChangeToHair();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && HaveMiner && !execute && !alpha2Pressed)
            {
                alpha2Pressed = true; // Marcar que Alpha2 ha sido presionada.
                alpha1Pressed = false;
                alpha3Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX, transform.rotation * rotationVFX);
                obj.transform.SetParent(transform);
                Destroy(obj, 1.4f);
                ChangeToMiner();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && HaveEngineer && !execute && !alpha3Pressed)
            {
                alpha3Pressed = true; // Marcar que Alpha3 ha sido presionada.
                alpha2Pressed = false;
                alpha1Pressed = false;
                GameObject obj = Instantiate(VFXchangeCharacter, transform.position + positionVFX, transform.rotation * rotationVFX);
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
            HaveMiner = true;

            Destroy(MinerBody);
        }

        if (isNearE && Input.GetKeyDown(KeyCode.F))
        {
            HaveEngineer = true;

            Destroy(EngineerBody);
        }
        if (isNearH && Input.GetKeyDown(KeyCode.F))
        {
            HaveHairdress = true;

            Destroy(HairdresserBody);
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
        EngineerSkin.SetActive(true);

        IsHairdress = false;
        IsMiner = false;
        IsEngineer = true;
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
