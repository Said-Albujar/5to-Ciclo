using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{


    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PanelDeactivate()
    {
        this.gameObject.SetActive(false);
    }

}
