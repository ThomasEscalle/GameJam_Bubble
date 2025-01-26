using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public Image helpImage;
    public Text helpText;
    

    public void setHelp( string text) {
        StartCoroutine(showHelp(text));
    }

    public IEnumerator showHelp(string text) {
        helpText.text = text;

        /// Set the alpha of the image to 0
        helpImage.color = new Color(1, 1, 1, 0);
        helpText.color = new Color(0, 0, 0, 0);

        Debug.Log("Showing help");
        /// LeanTween the alpha of the image
        LeanTween.alpha(helpImage.rectTransform, 1, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alphaText(helpText.rectTransform, 1, 0.5f).setEase(LeanTweenType.easeOutCubic);


        helpImage.gameObject.SetActive(true);
        helpText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        LeanTween.alpha(helpImage.rectTransform, 0, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => {
            helpImage.gameObject.SetActive(false);
            helpText.gameObject.SetActive(false);
        });

        LeanTween.alphaText(helpText.rectTransform, 0, 0.5f).setEase(LeanTweenType.easeOutCubic);

    }


}
