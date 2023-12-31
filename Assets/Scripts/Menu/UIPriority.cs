using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class UIPriority : MonoBehaviour
{

    public CinemachineVirtualCamera currentCamera;

    void Start()
    {
        currentCamera.Priority++;
        Cursor.lockState = CursorLockMode.None;

    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;
    }
}
