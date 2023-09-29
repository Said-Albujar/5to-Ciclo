using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Configuration : MonoBehaviour
{
    [Header("Volume")]
    public Slider sliderVolume;
    public float sliderValue;
    [Header("Glow")]
    public Slider sliderGlow;
    public float sliderValueGlow;
    public Image imageGlow;
    // Start is called before the first frame update
    void Start()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volume", 0.5f);
        AudioListener.volume = sliderVolume.value;

        sliderGlow.value = PlayerPrefs.GetFloat("glow", 0.5f);
        imageGlow.color = new Color(imageGlow.color.r, imageGlow.color.g, imageGlow.color.b, sliderGlow.value);
    }
    public void changeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = sliderVolume.value;

    }
    public void changeSliderGlow(float value)
    {
        sliderValueGlow = value;
        PlayerPrefs.SetFloat("glow", sliderValueGlow);
        imageGlow.color = new Color(imageGlow.color.r, imageGlow.color.g, imageGlow.color.b, sliderGlow.value);
    }
        



   

   
}
