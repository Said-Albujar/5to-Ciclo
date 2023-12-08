using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public MainMenu menuPanel;
    
    public void ButtonMenuActive()
    {
        menuPanel.panelCredits.SetActive(false);
    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
