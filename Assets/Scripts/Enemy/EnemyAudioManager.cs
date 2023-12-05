using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public AudioSource audioSource;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioSource.volume != audioManager.sliderMaster.value)
        {
            audioSource.volume = audioManager.sliderMaster.value;
        }
    }

    public void PlayStepSound()
    {
        PlaySound("Steps");
    }

    public void PlaySound(string name)
    {
        Sounds s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            audioSource.PlayOneShot(s.clip);
        }
    }

    public void DetecPlayer()
    {
        PlaySound("DetecPlayer");
    }
}
