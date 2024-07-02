using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyDButons : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public List<GameObject> objectsToDeactivate;

    public void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
    public void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
