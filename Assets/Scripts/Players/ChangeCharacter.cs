using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    public bool CanChange = true;
    public float TimeLeft = 0f;
    public float WaitTime = 5f;

    [Header("Hairdresser")]
    public GameObject HairdresserSkin;
    public bool IsHairdress;

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

    [Header("Engineer")]
    public GameObject MatadorSkin;
    public GameObject MatadorBody;
    public bool isNearT;
    public bool IsMatador;
    public bool HaveMatador;

    void Start()
    {
        ChangeToHair();
    }   
    
    void Update()
    {
        if (CanChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeToHair();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && HaveMiner)
            {
                ChangeToMiner();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && HaveEngineer)
            {
                ChangeToEngineer();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && HaveMatador)
            {
                ChangeToMatador();
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
        if(isNearT && Input.GetKeyDown(KeyCode.F))
        {
            HaveMatador = true;
            Destroy(MatadorBody);
        }
    }

    void ChangeToHair()
    {
        StartTimer();
        HairdresserSkin.SetActive(true);
        MinerSkin.SetActive(false);
        EngineerSkin.SetActive(false);
        MatadorSkin.SetActive(false);

        IsHairdress = true;
        IsMiner = false;
        IsEngineer = false;
        IsMatador = false;

    }

    void ChangeToMiner()
    {
        StartTimer();
        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(true);
        EngineerSkin.SetActive(false);
        MatadorSkin.SetActive(false);

        IsHairdress = false;
        IsMiner = true;
        IsEngineer = false;
        IsMatador = false;

    }
    void ChangeToEngineer()
    {
        StartTimer();
        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(false);
        EngineerSkin.SetActive(true);
        MatadorSkin.SetActive(false);

        IsHairdress = false;
        IsMiner = false;
        IsEngineer = true;
        IsMatador = false;
    }

    void ChangeToMatador()
    {
        StartTimer();
        HairdresserSkin.SetActive(false);
        MinerSkin.SetActive(false);
        EngineerSkin.SetActive(false);
        MatadorSkin.SetActive(true);

        IsHairdress = false;
        IsMiner = false;
        IsEngineer = false;
        IsMatador = true;
    }
    void StartTimer()
    {
        TimeLeft = WaitTime;
        CanChange = false;
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

        if (other.CompareTag("B_Mata"))
        {
            isNearT = true;
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

        if (other.CompareTag("B_Mata"))
        {
            isNearT = false;
        }
    }
}
