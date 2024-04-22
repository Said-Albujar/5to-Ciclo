using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel1 : MonoBehaviour
{
    bool once = false;

    // Update is called once per frame
    void Update()
    {
        if (once == false)
        {
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.SaveGame();
                once = true;
            }
        }
    }
}
