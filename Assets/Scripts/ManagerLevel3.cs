using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel3 : MonoBehaviour
{
    public Transform initialPos;
    bool once = false;

    private void Update()
    {
        if (once == false)
        {
            if (DataPersistenceManager.instance!=null)
            {
                DataPersistenceManager.instance.LoadGame();
                PlayerMovement.Instance.transform.position = initialPos.position;
                DataPersistenceManager.instance.SaveGame();
                once = true;
            }
        }
    }

}
