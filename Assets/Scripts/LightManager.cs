using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] LightList[] lights;
    [SerializeField] GameObject[] allLight;

    public void EnabledLigth(int tp)
    {
        if(allLight.Length > 0)
        {
            foreach (var item in allLight)
            {
                item.SetActive(false);
            }
        }

        switch(tp)
        {
            case 0 :
                foreach (var item in lights[0].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;

            case 1 :
                foreach (var item in lights[1].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;

            case 2 :
                foreach (var item in lights[2].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;

            case 3 :
                foreach (var item in lights[3].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;

            case 4 :
                foreach (var item in lights[4].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;

            case 5 :
                foreach (var item in lights[5].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;
            case 6 :
                foreach (var item in lights[6].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;
            case 7 :
                foreach (var item in lights[7].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;
            case 8 :
                foreach (var item in lights[8].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;
            case 9 :
                foreach (var item in lights[9].listLight)
                {
                    if(item != null)
                    item.SetActive(true);
                }
            break;
        }
    }
}
