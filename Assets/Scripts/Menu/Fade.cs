using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fade: MonoBehaviour
{
    public TextMeshProUGUI textToFade;
    public float fadeDuration = 2.0f;
    private bool fading = false;
    void Start()
    {
        StartFade();
    }
    public void StartFade()
    {
        if (textToFade != null && !fading)
        {
            fading = true;
            StartCoroutine(FadeTextGradually());
        }
    }
    private System.Collections.IEnumerator FadeTextGradually()
    {
        Color textColor = textToFade.color;
        float startTime = Time.time;

        while (Time.time - startTime < fadeDuration)
        {
            textColor.a = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeDuration);
            textToFade.color = textColor;
            yield return null;
        }
        fading = false;
    }
}