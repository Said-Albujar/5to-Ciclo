using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PassWordUI : MonoBehaviour
{
    public GameObject panelTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("TargetPlayer"))
        {
            Debug.Log("asdasdasasd");
            panelTransition.SetActive(true);
            DataPersistenceManager.instance.SaveGame();
            
        }
    }
    public void SceneFadeIn()
    {
        panelTransition.SetActive(true);

    }
}
