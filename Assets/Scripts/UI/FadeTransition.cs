using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeTransition : MonoBehaviour
{

    public static FadeTransition Instance;

    public Image mImage;

    /// The list of sprites to fad in
    public List<Sprite> mSprites_FadeIn = new List<Sprite>();

    /// The list of sprites to fad out
    public List<Sprite> mSprites_FadeOut = new List<Sprite>();


    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        /// Set the instance
        Instance = this;
        /// Get the image component
        mImage = GetComponent<Image>();
    }

    /// Fade in
    /// @param[in] _time The time to fade in
    /// @param[in] _callback The callback function
    public void FadeIn(float _time, System.Action _callback)
    {
        Debug.Log("FadeIn");
        StartCoroutine(FadeInCoroutine(_time, _callback));
    }

    /// Fade out
    /// @param[in] _time The time to fade out
    /// @param[in] _callback The callback function
    public void FadeOut(float _time, System.Action _callback)
    {
        Debug.Log("FadeOut");
        StartCoroutine(FadeOutCoroutine(_time, _callback));
    }

    /// Fade in coroutine
    /// @param[in] _time The time to fade in
    /// @param[in] _callback The callback function
    private IEnumerator FadeInCoroutine(float _time, System.Action _callback)
    {

        Debug.Log("FadeInCoroutine");
        /// Set the image to the first sprite
        mImage.sprite = mSprites_FadeIn[0];

        /// The time passed
        float timePassed = 0.0f;
        float timeSinceLast = 0.0f;

        mImage.enabled = true;

        /// The index of the sprite
        int index = 0;

        /// The time to wait
        float timeToWait = _time / mSprites_FadeIn.Count;

        /// While the time passed is less than the time
        while (timePassed <= _time)
        {
            /// Increase the time passed
            timePassed += Time.deltaTime;
            timeSinceLast += Time.deltaTime;

            /// If the time passed is greater than the time to wait
            if (timeSinceLast > timeToWait)
            {
                /// Increase the index
                index++;

                /// If the index is greater than the count
                if (index >= mSprites_FadeIn.Count)
                {
                    /// Set the index to the last index
                    index = mSprites_FadeIn.Count - 1;
                }

                /// Set the sprite
                mImage.sprite = mSprites_FadeIn[index];

                timeSinceLast = 0;
            }


            /// Wait for the end of frame
            yield return new WaitForEndOfFrame();
        }



        /// If the callback is not null
        if (_callback != null)
        {
            /// Call the callback
            _callback();
        }
        mImage.enabled = true;

        Debug.Log("Callback");
        yield return null;
    }


    /// Fade out coroutine
    /// @param[in] _time The time to fade out
    /// @param[in] _callback The callback function
    private IEnumerator FadeOutCoroutine(float _time, System.Action _callback)
    {
        /// Set the image to the first sprite
        mImage.sprite = mSprites_FadeOut[0];

        /// The time passed
        float timePassed = 0;
        float timeSinceLast = 0;

        mImage.enabled = true;

        /// The index of the sprite
        int index = 0;

        /// The time to wait
        float timeToWait = _time / mSprites_FadeOut.Count;

        /// While the time passed is less than the time
        while (timePassed <= _time)
        {
            /// Increase the time passed
            timePassed += Time.deltaTime;
            timeSinceLast += Time.deltaTime;

            /// If the time passed is greater than the time to wait
            if (timeSinceLast > timeToWait)
            {
                /// Increase the index
                index++;

                /// If the index is greater than the count
                if (index >= mSprites_FadeOut.Count)
                {
                    /// Set the index to the last index
                    index = mSprites_FadeOut.Count - 1;
                }

                /// Set the sprite
                mImage.sprite = mSprites_FadeOut[index];

                timeSinceLast = 0;
            }

            /// Wait for the end of frame
            yield return new WaitForEndOfFrame();
        }

        mImage.enabled = false;

        /// If the callback is not null
        if (_callback != null)
        {
            /// Call the callback
            _callback();

        }

        Debug.Log("Callback");
        yield return null;
    }
}
