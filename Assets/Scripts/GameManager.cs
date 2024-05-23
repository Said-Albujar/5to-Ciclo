using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerMovement;
    public ChangeCharacter changeCharacter;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelUpgrades;
    [SerializeField] private GameObject panelOptions;
    [SerializeField] private string sceneName;
    public bool inPause;
    public GameObject panelTransition;
    public GameObject panelConsole;
    [SerializeField] LightManager lightManager;

    public GameObject[] tps = new GameObject[9];
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

        ConsoleMenu();
        
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
        panelUpgrades.SetActive(false);
        inPause = false;
    }

    public void Restart()
    {

        Time.timeScale = 1f;
        panelTransition.SetActive(true);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Inicio");
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
        SceneManager.LoadScene("MainMenu");
    }

    public void ConsoleMenu()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !panelOptions.activeSelf)
        {
            switch (panelConsole.activeSelf)
            {
                case true:
                    panelConsole.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                case false:
                    panelConsole.SetActive(true);
                    Time.timeScale = 0f;
                    break;
            }
        }

        if (panelConsole.activeSelf )
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) ||
                Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) ||
                Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) ||
                Input.GetKeyDown(KeyCode.Alpha9) && sceneName == "Tutorial and level1")
            {
                char keyPressed = Input.inputString[0];
                int teleportIndex = int.Parse(keyPressed.ToString()) - 1;
                
                if (teleportIndex >= 0 && teleportIndex < tps.Length)
                {
                    if (lightManager != null)
                    {
                        lightManager.EnabledLigth(teleportIndex);
                    }
                    
                    playerMovement.transform.position = tps[teleportIndex].transform.position;
                    Time.timeScale = 1f;
                    panelConsole.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {

                GiveAllTools();
                DataPersistenceManager.instance.SaveGame();
                Time.timeScale = 1f;
                panelConsole.SetActive(false);
            }
        }
    }

    public void GiveAllTools()
    {
        changeCharacter.HaveEngineer = true;
        changeCharacter.HaveHairdress = true;
        changeCharacter.HaveMiner = true;
    }
}
