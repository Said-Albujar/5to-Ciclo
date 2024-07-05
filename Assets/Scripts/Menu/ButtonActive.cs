using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActive : MonoBehaviour
{
    //public GameObject panelPause;
    public GameObject panelExit;
    //public PlayerHealth player;
    public void ButtonMenuActive()
    {
        CollectObject.regenerateStamina = false;
        //panelPause.SetActive(true);
        
    }
    public void ButtonGoOut()
    {
        //player.turn = false;
        //player.GetComponent<Rigidbody>().isKinematic = true;
        panelExit.SetActive(false);
        //Time.timeScale = 1f;

    }

    public void Exit()
    {
        panelExit.SetActive(true);
        Time.timeScale = 1f;
    }

    /*public void DataManagerEvent()
    {
        player.DataManager();
    }*/
    /*public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }*/

}
