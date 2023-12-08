using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Animator anim,anim2;
    [SerializeField] private string sceneName;
    public GameObject panelCredits;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panelCredits.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (Input.anyKeyDown && sceneName == "Menu")
        {
            anim.SetBool("pressEnter", true);
        }
    }

    public void ButtonStart()
    {
        anim.SetBool("start", true);
    }
    public void ButtonCredists()
    {
        panelCredits.SetActive(true);
    }
    public void ButtonMenu()
    {
        //panelCredits.SetActive(false);
        anim2.SetBool("menu", true);


    }

    public void ButtonExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

  
}
