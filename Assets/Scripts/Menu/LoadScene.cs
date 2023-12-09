using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelExit;
    public PlayerMovement player;
    public void ButtonMenuActive()
    {
        panelPause.SetActive(true);
        
    }
    public void ButtonGoOut()
    {
        //player.turn = false;
        //player.GetComponent<Rigidbody>().isKinematic = true;
        panelExit.SetActive(true);
        Time.timeScale = 1f;

    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
