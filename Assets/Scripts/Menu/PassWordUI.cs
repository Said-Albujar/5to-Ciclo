using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PassWordUI : MonoBehaviour
{
    public GameObject textPassWord;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textPassWord.SetActive(true);
        }
    }
}
