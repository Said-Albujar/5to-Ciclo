using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneByName(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
