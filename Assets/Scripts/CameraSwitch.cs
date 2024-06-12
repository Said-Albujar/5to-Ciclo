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
    [SerializeField]private bool isTransitioning = false;
    [SerializeField] private bool isTransitioning1 = true;
    [SerializeField] private bool isTransitioning2 = true;
    [SerializeField] private bool isTransitioning3 = true;

    public ButtonMecanism buttonMecanism;
    public ButtonMecanism buttonMecanis1;
    public ButtonMecanism buttonMecanis2;

    public GameObject cameraTransition;
    public GameObject cameraTransition1;
    public GameObject cameraTransition2;
    public GameObject cameraTransition3;
    public GameObject cameraTransition4;
    public GameObject cameraTransition5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone") && !isTransitioning)
        {
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        isTransitioning = true;
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
        isTransitioning = false;
    }
    IEnumerator SwitchCamera2()
    {
        mainCamera.SetActive(false);
        cameraTransition.SetActive(true);
        cameraTransition1.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition.transform, cameraTransition1.transform.position, 3.5f));
        mainCamera.SetActive(true);
        cameraTransition.SetActive(false);
        cameraTransition1.SetActive(false);

        yield return new WaitForSeconds(3f);
        //box.transform.position = positionBox.transform.position;

        isTransitioning1 = false;

    }
    IEnumerator SwitchCamera3()
    {
        mainCamera.SetActive(false);
        cameraTransition1.SetActive(true);
        cameraTransition2.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition3.transform, cameraTransition2.transform.position, 3.5f));
        mainCamera.SetActive(true);
        cameraTransition1.SetActive(false);
        cameraTransition2.SetActive(false);

        yield return new WaitForSeconds(3f);
        //box.transform.position = positionBox.transform.position;

        isTransitioning2 = false;

    }
    IEnumerator SwitchCamera4()
    {
        mainCamera.SetActive(false);
        cameraTransition4.SetActive(true);
        cameraTransition5.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition3.transform, cameraTransition2.transform.position, 3.5f));
        mainCamera.SetActive(true);
        cameraTransition4.SetActive(false);
        cameraTransition5.SetActive(false);

        yield return new WaitForSeconds(3f);
        //box.transform.position = positionBox.transform.position;

        isTransitioning3 = false;

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
    private void LateUpdate()
    {
        TransitionCamera();
    }
    public void TransitionCamera()
    {
        if(buttonMecanism!=null)
        {
            if (buttonMecanism.buttonPressed&&isTransitioning1)
            {
                StartCoroutine(SwitchCamera2());

            }
            if (buttonMecanis1.buttonPressed && isTransitioning2)
            {
                StartCoroutine(SwitchCamera3());

            }
            if (buttonMecanis2.buttonPressed && isTransitioning3)
            {
                StartCoroutine(SwitchCamera4());

            }
            if(buttonMecanism.buttonPressed&&buttonMecanis1.buttonPressed)
            {
                StopCoroutine(SwitchCamera2());
                StopCoroutine(SwitchCamera3());

            }
        }
       
    }
}
