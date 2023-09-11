using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera cameraGofer;
    public Camera cameraChef;
    public AudioListener goferAudio;
    public AudioListener chefAudio;
    public GameObject cameraGoferMini;
    public GameObject cameraChefMini;

    public KeyCode toggleKey;

    public enum Characters
    {
        Gofer,
        Chef
    }

    public Characters character;

    void Start()
    {
        character = Characters.Gofer;
        SetCharacterCamera();
    }

    void Update()
    {
        if(Input.GetKeyDown(toggleKey))
        {
            if(character == Characters.Gofer) character = Characters.Chef;
            else character = Characters.Gofer;

            SetCharacterCamera();
        }
    }

    void SetCharacterCamera()
    {
        switch (character)
        {
            case Characters.Gofer:
                cameraGofer.enabled = true;
                goferAudio.enabled = true;
                cameraGoferMini.SetActive(false);
                cameraChef.enabled = false;
                chefAudio.enabled = false;
                cameraChefMini.SetActive(true);
                break;
            case Characters.Chef:
                cameraChef.enabled = true;
                chefAudio.enabled = true;
                cameraChefMini.SetActive(false);
                cameraGofer.enabled = false;
                goferAudio.enabled = false;
                cameraGoferMini.SetActive(true);
                break;
        }
    }
}
