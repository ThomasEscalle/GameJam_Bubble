using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeTransition : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 2f;

    public IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return 0;
        }

        /// Destroy the game object
        Destroy(gameObject);
    }

    public IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return 0;
        }

        /// Destroy the game object
        Destroy(gameObject);
    }


    public void Awake()
    {
        fadeImage = GetComponent<Image>();
        StartCoroutine(FadeIn());
    }
}
