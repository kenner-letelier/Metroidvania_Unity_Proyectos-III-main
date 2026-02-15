using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] float fadeSpeed;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void FadeIn()
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }


        StartCoroutine(_FadeIn());

    }

    public IEnumerator _FadeIn()
    {
        canvasGroup.alpha = 0;
        var alpha = canvasGroup.alpha;
        while (alpha < 1)
        {
            alpha += fadeSpeed;
            canvasGroup.alpha = alpha;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        canvasGroup.alpha = 1;
    }

 

    public void FadeOut()
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        StartCoroutine(_FadeOut());

    }

    public IEnumerator _FadeOut()
    {
        canvasGroup.alpha = 1;
        var alpha = canvasGroup.alpha;
        while (alpha > 0)
        {
            alpha -= fadeSpeed;
            canvasGroup.alpha = alpha;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        canvasGroup.alpha = 0;
    }

    
}

