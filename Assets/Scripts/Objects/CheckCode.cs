using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCode : MonoBehaviour
{
    [SerializeField] Activator activator;
    [SerializeField] InteractiveDoor door;
    [SerializeField] Candado[] candados;
    [SerializeField] int[] correctCode;
    [SerializeField] int countCorrect = 0;

    
    public void Check()
    {
        countCorrect = 0;
        for (int i = 0; i < candados.Length; i++)
        {
            Debug.Log(candados[i]);
            if(candados[i].currenNumber == correctCode[i])
            {
                countCorrect++;
            }
            else return;
        }

        if(countCorrect == 4)
        {
            door.openDoor = true;
            activator.ContinueGame();
            gameObject.SetActive(false);
        }
    }
}
