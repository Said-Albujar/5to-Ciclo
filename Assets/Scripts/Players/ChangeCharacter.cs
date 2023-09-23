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
    public bool IsMiner;

    [Header("Engineer")]
    public GameObject EngineerSkin;
    public bool IsEngineer;

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

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeToMiner();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
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
    }

}
