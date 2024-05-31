using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candado : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float velocityRitate;
    public int currenNumber = 1;
    bool canRotate = true;

    public void Execute(int direction)
    {
        if(canRotate)
        {
            currenNumber += direction;

            if(currenNumber < 1)
            currenNumber = 9;
            else if(currenNumber > 9)
            currenNumber = 1;
            
            canRotate = false;
            Quaternion rotacionObjetivo = transform.rotation * Quaternion.Euler(0, 0, 40 * -direction);

            StartCoroutine(Rotate(rotacionObjetivo));
        }
    }

    private IEnumerator Rotate(Quaternion rotacionObjetivo)
    {
        audioSource.PlayOneShot(sfx);
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
