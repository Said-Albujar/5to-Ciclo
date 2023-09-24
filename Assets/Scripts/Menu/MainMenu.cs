using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            anim.SetBool("pressEnter", true);
        }
    }

    public void ButtonStart()
    {
        anim.SetBool("start", true);
    }
    public void ButtonExitGame()
    {
        Application.Quit();
    }
}
