using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCInematicaController : MonoBehaviour
{
    public Sounds[] sounds;
    public AudioSource audioSource;
  

    // Update is called once per frame
 

    public void PlayStepSound()
    {
        //PlaySound("Walk");
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
    public void SoundBossPain()
    {
        PlaySound("SonidoDolor");
    }
    public void SoundPlayerSorcerer()
    {
        PlaySound("SonidoSuspiro");

    }
}
