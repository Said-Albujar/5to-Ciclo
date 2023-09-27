using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Configuration : MonoBehaviour
{
    [Header("Volume")]
    public Slider sliderVolume;
    public float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volume", 0.5f);
        AudioListener.volume = sliderVolume.value;
    }
    public void changeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = sliderVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
