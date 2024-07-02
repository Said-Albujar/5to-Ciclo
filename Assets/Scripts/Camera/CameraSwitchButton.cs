using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraSwitchButton : MonoBehaviour


{
    public GameObject mainCamera;

   


    public ButtonMecanism buttonMecanism;
    public ButtonMecanism buttonMecanis1;
    public ButtonMecanism buttonMecanis2;

    public GameObject cameraTransition;
    public GameObject cameraTransition1;
    public GameObject cameraTransition2;
    public GameObject cameraTransition3;
    public GameObject cameraTransition4;
    public GameObject cameraTransition5;
    public GameObject cameraTransition6;
    public GameObject cameraTransition7;
    public GameObject cameraTransition8;
    public GameObject cameraTransition9;
    public  bool isTransitioning1 = true;
    public  bool isTransitioning2 = true;
    public  bool isTransitioning3 = true;
    public  bool isTransitioning4 = true;
    public  bool isTransitioning5 = true;


    private void LateUpdate()
    {
        TransitionCamera();
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
    IEnumerator SwitchCamera2()
    {
        if (isTransitioning1)
        {
            mainCamera.SetActive(false);
            cameraTransition.SetActive(true);
            cameraTransition1.SetActive(true);

            yield return StartCoroutine(MoveCamera(cameraTransition.transform, cameraTransition1.transform.position, 1.5f));
            mainCamera.SetActive(true);
            cameraTransition.SetActive(false);
            cameraTransition1.SetActive(false);

            yield return new WaitForSeconds(1.5f);
            //box.transform.position = positionBox.transform.position;

            isTransitioning1 = false;

        }


    }
    IEnumerator SwitchCamera3()
    {
        if (isTransitioning2)
        {
            mainCamera.SetActive(false);
            cameraTransition1.SetActive(true);
            cameraTransition2.SetActive(true);

            yield return StartCoroutine(MoveCamera(cameraTransition3.transform, cameraTransition2.transform.position, 1.5f));
            mainCamera.SetActive(true);
            cameraTransition1.SetActive(false);
            cameraTransition2.SetActive(false);

            yield return new WaitForSeconds(1.5f);
            //box.transform.position = positionBox.transform.position;

            isTransitioning2 = false;
        }


    }
    IEnumerator SwitchCamera4()
    {
        mainCamera.SetActive(false);
        cameraTransition4.SetActive(true);
        cameraTransition5.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition3.transform, cameraTransition2.transform.position, 1.5f));
        mainCamera.SetActive(true);
        cameraTransition4.SetActive(false);
        cameraTransition5.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        //box.transform.position = positionBox.transform.position;

        isTransitioning3 = false;

    }
    IEnumerator SwitchCamera5()
    {
        mainCamera.SetActive(false);
        cameraTransition6.SetActive(true);
        cameraTransition7.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition6.transform, cameraTransition7.transform.position, 1.5f));
        mainCamera.SetActive(true);
        cameraTransition6.SetActive(false);
        cameraTransition7.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        //box.transform.position = positionBox.transform.position;
        isTransitioning4 = false;

    }
    IEnumerator SwitchCamera6()
    {
        mainCamera.SetActive(false);
        cameraTransition8.SetActive(true);
        cameraTransition9.SetActive(true);

        yield return StartCoroutine(MoveCamera(cameraTransition8.transform, cameraTransition9.transform.position, 1.5f));
        mainCamera.SetActive(true);
        cameraTransition7.SetActive(false);
        cameraTransition9.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        //box.transform.position = positionBox.transform.position;
        isTransitioning5 = false;

    }

    public void TransitionCamera()
    {
        if (buttonMecanism != null)
        {
            if (buttonMecanism.buttonPressed && isTransitioning1 && !buttonMecanis1.buttonPressed)
            {

                StartCoroutine(SwitchCamera2());

            }
            if (buttonMecanis1.buttonPressed && isTransitioning2 && !buttonMecanism.buttonPressed)
            {

                StartCoroutine(SwitchCamera3());

            }
            if (buttonMecanis2.buttonPressed && isTransitioning3)
            {
                StartCoroutine(SwitchCamera4());

            }
            if (!isTransitioning1 && buttonMecanis1.buttonPressed && isTransitioning4)
            {
                StartCoroutine(SwitchCamera5());

            }
            if (!isTransitioning2 && buttonMecanism.buttonPressed && isTransitioning5)
            {
                StartCoroutine(SwitchCamera6());

            }


        }

    }
}
