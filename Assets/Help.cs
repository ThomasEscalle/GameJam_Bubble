using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public Image helpImage;
    public Text helpText;


    public void setHelp( string text, Sprite image) {
        helpText.text = text;
        helpImage.sprite = image;
        helpImage.gameObject.SetActive(true);
        helpText.gameObject.SetActive(true);
    }

    public IEnumerator showHelp(string text, Sprite image) {
        helpText.text = text;
        helpImage.sprite = image;

        /// LeanTween the alpha of the image
        LeanTween.alpha(helpImage.rectTransform, 1, 0.5f).setEase(LeanTweenType.easeOutCubic);



        helpImage.gameObject.SetActive(true);
        helpText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        LeanTween.alpha(helpImage.rectTransform, 0, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => {
            helpImage.gameObject.SetActive(false);
            helpText.gameObject.SetActive(false);
        });
    }


}
