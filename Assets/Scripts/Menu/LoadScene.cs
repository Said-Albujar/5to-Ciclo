using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByName(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
