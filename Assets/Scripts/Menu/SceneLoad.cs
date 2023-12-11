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
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PanelDeactivate()
    {
        this.gameObject.SetActive(false);
    }

}
