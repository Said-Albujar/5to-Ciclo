using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelOptions;
    [SerializeField] private string sceneName;
    public bool inPause;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            DataPersistenceManager.instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DataPersistenceManager.instance.LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !panelOptions.activeSelf)
        {
            switch (GetInPause())
            {
                case true:
                    Unpause();
                    break;
                case false:
                    Pause();
                    break;
            }
        }
    }

    public  void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public  void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        ShowCursor();
        Time.timeScale = 0f;
        panelPause.SetActive(true);
        inPause = true;
    }

    public void Unpause()
    {
        HideCursor();
        Time.timeScale = 1f;
        panelPause.SetActive(false);
        inPause = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
        inPause = false;
    }
    public void Restaurar()
    {
        Time.timeScale = 1f;

        panelPause.SetActive(false);
        inPause = false;
    }

    public bool GetInPause()
    {
        return inPause;
    }

    public void ExitMenu()
    {
        Unpause();
        SceneManager.LoadScene("Menu");
    }
}
