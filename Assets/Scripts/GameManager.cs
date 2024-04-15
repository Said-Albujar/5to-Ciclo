using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerMovement;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelOptions;
    [SerializeField] private string sceneName;
    public bool inPause;
    public GameObject panelTransition;
    private void Awake()
    {
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
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }
    void Update()
    {
        PauseManager();
    }
    public void PauseActive()
    {
        inPause = false;
    }
    public void PauseManager()
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
        panelTransition.SetActive(true);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Inicio");
        inPause = false;

        //if(File.Exists(string.Concat(Application.persistentDataPath, "/data.game")))
        //{
        //    panelPause.SetActive(false);
        //    HideCursor();
        //    Debug.Log("Entro");
        //    DataPersistenceManager.instance.LoadGame();

        //}
        //else
        //{
        //    SceneManager.LoadScene(sceneName);
        //    Debug.Log("Inicio");
        //}
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
        SceneManager.LoadScene("MainMenu");
    }

}
