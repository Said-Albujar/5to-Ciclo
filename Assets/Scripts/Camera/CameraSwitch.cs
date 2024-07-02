    using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject camera;
    public Image fadeInImage;
    public Image fadeOutImage;
    public GameObject box;
    public Transform positionBox;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair") && GlobalStaticCamera.isTransitioning)
        {
            GlobalStaticCamera.isTransitioning = false;
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        camera.SetActive(true);
        yield return new WaitForSeconds(8f);

        yield return StartCoroutine(FadeImageToBlack(fadeInImage, 2f));
        
        camera.SetActive(false);
        box.transform.position = positionBox.transform.position;
        yield return new WaitForSeconds(1f);
        fadeInImage.gameObject.SetActive(false);
        yield return StartCoroutine(FadeImageToTransparent(fadeOutImage, 3f));
        
        box.transform.position = positionBox.transform.position;
        player.SetActive(true);
        
        //isTransitioning = false;
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

        
        image.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
   
  
}
