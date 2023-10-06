using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pause = false;
    public bool startPause;
    [SerializeField] private GameObject panelPause;
    public string sceneName;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


    }
    private void Update()
    {
        if (startPause)
        {
            pauseScene();
        }
    }
    void pauseScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {


            Time.timeScale = 0f;
            panelPause.SetActive(true);
            pause = true;
            Cursor.lockState = CursorLockMode.None;



        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            Time.timeScale = 1f;
            panelPause.SetActive(false);
            pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;



        }

    }
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
        pause = false;
    }
    public void Restaurar()
    {
        Time.timeScale = 1f;

        panelPause.SetActive(false);
        pause = false;
    }
    public void ExitMenu()
    {
        Application.Quit();

    }
   
   
}
