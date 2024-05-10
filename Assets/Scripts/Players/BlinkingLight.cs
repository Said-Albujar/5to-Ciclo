using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Light lightToBlink;
    public float onTime = 0.5f; 
    public float offTime = 0.5f; 
    private bool isLightOn = true; 
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (isLightOn && timer >= onTime)
        {
            lightToBlink.enabled = false;
            isLightOn = false;
            timer = 0f;
        }
        if (!isLightOn && timer >= offTime)
        {
            lightToBlink.enabled = true;
            isLightOn = true;
            timer = 0f;
        }
    }
}
