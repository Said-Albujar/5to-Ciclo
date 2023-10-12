using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            DataPersistenceManager.instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DataPersistenceManager.instance.LoadGame();
        }
    }
}
