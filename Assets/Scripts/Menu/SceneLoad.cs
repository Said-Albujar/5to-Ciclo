using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public bool active;
    public Material shaderDissolver;
    public float value = 20.8f;
    public bool activeDissolve;


    public void Start()
    {
        value = 20.8f;
     
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void Update()
    {
        if (shaderDissolver != null)
        {
            shaderDissolver.SetFloat("_dissolveAmount", value);
            if (activeDissolve)
            {
                value -= 2*Time.deltaTime;

            }
        }
      
       
    }
    public void ReturnBossShader()
    {
        value = 20.8f;
        activeDissolve = false;
    }
    public void ActiveShaderState()
    {
        activeDissolve = true;
    
    }
 
    public void loadScene1()
    {
        SceneManager.LoadScene("Level3");
    }
    public void loadScene2()
    {
        SceneManager.LoadScene("FinalCinematica");
    }
    public void loadScene3()
    {
        SceneManager.LoadScene("Tutorial and level1");
    }
    public void loadScene4()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PanelDeactivate()
    {
        this.gameObject.SetActive(false);
    }

}
