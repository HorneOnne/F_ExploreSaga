using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExploreSaga
{
    public class FadeTransition : MonoBehaviour
    {
        public static FadeTransition Instance{get;set;}
        public Image fadeImage;
        public float fadeDuration = 1.0f;
        [SerializeField] private bool fadeInWhenStart;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (fadeInWhenStart)
                FadeIn(null);
        }


        public void FadeIn(System.Action afterFadeIn)
        {
            StartCoroutine(PerfromFadeIn(afterFadeIn));
        }

        public void FadeOut(System.Action afterFadeOut)
        {
            StartCoroutine(PerformFadeOut(afterFadeOut));
        }



        private IEnumerator PerfromFadeIn(System.Action afterFadeIn)
        {
            fadeImage.gameObject.SetActive(true);
            Color color = fadeImage.color;
            float currentTime = fadeDuration;

            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                color.a = currentTime / fadeDuration;
                fadeImage.color = color;
                yield return null;
            }

            fadeImage.gameObject.SetActive(false);
            afterFadeIn?.Invoke();
        }

        private IEnumerator PerformFadeOut(System.Action afterFadeOut)
        {
            fadeImage.gameObject.SetActive(true);
            Color color = fadeImage.color;
            float currentTime = 0f;

            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                color.a = currentTime / fadeDuration;
                fadeImage.color = color;
                yield return null;
            }

            afterFadeOut?.Invoke();
        }
    }

}
