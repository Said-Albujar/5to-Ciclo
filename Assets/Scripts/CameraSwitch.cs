using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject firstTransitionCamera;
    public GameObject secondTransitionCamera;
    public GameObject finalCamera;
    public GameObject Collider;
    public Image fadeInImage;
    public Image fadeOutImage;
    public GameObject box;
    public Transform positionBox;








    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone") && GlobalStaticCamera.isTransitioning)
        {
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        Collider.SetActive(false);
        mainCamera.SetActive(false);
        firstTransitionCamera.SetActive(true);

        yield return StartCoroutine(MoveCamera(firstTransitionCamera.transform, secondTransitionCamera.transform.position, 3.5f));

        firstTransitionCamera.SetActive(false);
        secondTransitionCamera.SetActive(true);

        yield return StartCoroutine(MoveCamera(secondTransitionCamera.transform, finalCamera.transform.position, 3.5f));

        secondTransitionCamera.SetActive(false);
        finalCamera.SetActive(true);

        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeImageToBlack(fadeInImage, 2f));

        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeImageToTransparent(fadeOutImage, 5f));

        mainCamera.SetActive(true);
        finalCamera.SetActive(false);
        firstTransitionCamera.SetActive(false);
        secondTransitionCamera.SetActive(false);

        yield return new WaitForSeconds(1f);
        box.transform.position = positionBox.transform.position;
        GlobalStaticCamera.isTransitioning = false;

        //isTransitioning = false;
    }


    IEnumerator MoveCamera(Transform cameraTransform, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = cameraTransform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPosition;
    }

    IEnumerator FadeImageToBlack(Image image, float duration)
    {
        float startAlpha = 0;
        float endAlpha = 1;
        float elapsedTime = 0;

        Color color = image.color;
        color.a = startAlpha;
        image.color = color;
        image.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }

    IEnumerator FadeImageToTransparent(Image image, float duration)
    {
        float startAlpha = 1;
        float endAlpha = 0;
        float elapsedTime = 0;

        Color color = image.color;
        color.a = startAlpha;
        image.color = color;
        image.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;

        if (endAlpha == 0)
        {
            image.gameObject.SetActive(false);
        }
    }
   
  
}
