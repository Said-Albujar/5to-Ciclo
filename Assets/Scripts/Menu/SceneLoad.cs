using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public bool active;
    public void Start()
    {
        if(active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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
    public void PanelDeactivate()
    {
        this.gameObject.SetActive(false);
    }

}
