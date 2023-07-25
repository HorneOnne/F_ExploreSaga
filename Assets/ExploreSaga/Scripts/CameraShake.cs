using System.Collections;
using UnityEngine;

namespace ExploreSaga
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get;set; }
        public float shakeDuration = 0.3f;
        public float shakeIntensity = 0.1f;

        private Vector3 originalPosition;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            originalPosition = transform.position;
        }

        public void Shake()
        {
            if (shakeDuration > 0)
            {
                StopAllCoroutines();
                StartCoroutine(ShakeCoroutine());
            }
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                Vector3 shakeOffset = Random.insideUnitCircle * shakeIntensity;
                transform.position = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0f);

                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            transform.position = originalPosition;
        }
    }

}
