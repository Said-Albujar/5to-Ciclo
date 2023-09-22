using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    public bool CanChange = true;
    public float TimeLeft = 0f;
    public float WaitTime = 5f;

    [Header("Hairdresser")]
    public Renderer HairdresserSkin;
    public bool IsHairdress;

    [Header("Miner")]
    public Renderer MinerSkin;
    public bool IsMiner;

    [Header("Engineer")]
    public Renderer EngineerSkin;
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
        HairdresserSkin.material.color = Color.red;
       
        IsHairdress = true;
        IsMiner = false;
        IsEngineer = false;
    }

    void ChangeToMiner()
    {
        StartTimer();
        MinerSkin.material.color = Color.blue;

        IsHairdress = false;
        IsMiner = true;
        IsEngineer = false;
    }
    void ChangeToEngineer()
    {
        StartTimer();
        EngineerSkin.material.color = Color.green;

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
