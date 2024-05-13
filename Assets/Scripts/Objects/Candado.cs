using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candado : MonoBehaviour
{
    [SerializeField] float velocityRitate;
    public int currenNumber = 1;
    bool canRotate = true;

    public void Execute()
    {
        if(canRotate)
        {
            if(currenNumber < 9)
            {
                currenNumber++;
            }
            else
            {
                currenNumber = 1;
            }
            canRotate = false;
            Quaternion rotacionObjetivo = transform.rotation * Quaternion.Euler(0, 0, 40);

            StartCoroutine(Rotate(rotacionObjetivo));
        }
    }

    private IEnumerator Rotate(Quaternion rotacionObjetivo)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * velocityRitate;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, t);
            yield return null;
        }

        transform.rotation = rotacionObjetivo;
        canRotate = true;
    }
}
