using System.Collections;
using UnityEngine;

namespace ExploreSaga
{
    public class SplitLine : MonoBehaviour
    {
        public static event System.Action<SplitType, Vector2, Vector2> OnLineSpreadCompleted;

        [Header("References")]
        [SerializeField] private OneSideScaleDirection part01;
        [SerializeField] private OneSideScaleDirection part02;
        [SerializeField] private SpriteRenderer circle;


        // Cached
        Vector3 targetScaleA = Vector3.zero;
        Vector3 targetScaleB = Vector3.zero;
        public void Spread(Vector3 hitPointA, Vector3 hitPointB, SplitType splitType, float timeSpread)
        {
      
            switch (splitType)
            {
                default: break;
                case SplitType.Horizontal:
                    float distanceToLeft = Vector2.Distance(part01.transform.position, hitPointA);
                    float distanceToRight = Vector2.Distance(part02.transform.position, hitPointB);
                    targetScaleA = new Vector3(distanceToLeft, 1.0f, 1.0f);
                    targetScaleB = new Vector3(distanceToRight, 1.0f, 1.0f);
                    ScaleToTarget(part01.transform, targetScaleA, timeSpread);
                    ScaleToTarget(part02.transform, targetScaleB, timeSpread);

                    StartCoroutine(Utilities.WaitAfter(timeSpread, () =>
                    {
                        OnLineSpreadCompleted?.Invoke(splitType, hitPointA, hitPointB);
                    }));
                    break;
                case SplitType.Vertical:
                    float distanceToUp = Vector2.Distance(part01.transform.position, hitPointA);
                    float distanceToDown = Vector2.Distance(part02.transform.position, hitPointB);
                    targetScaleA = new Vector3(distanceToUp, 1.0f, 1.0f);
                    targetScaleB = new Vector3(distanceToDown, 1.0f, 1.0f);
                    ScaleToTarget(part01.transform, targetScaleA, timeSpread);
                    ScaleToTarget(part02.transform, targetScaleB, timeSpread);

                    StartCoroutine(Utilities.WaitAfter(timeSpread, () =>
                    {
                        OnLineSpreadCompleted?.Invoke(splitType, hitPointA, hitPointB);
                    }));

                    Debug.Log($"{distanceToUp}\t{distanceToDown}");
                    break;
            }
        }

       
        public void ScaleToTarget(Transform objectTrans, Vector2 targetScale, float animationDuration)
        {
            StartCoroutine(Scale(objectTrans, targetScale, animationDuration));
        }

        private IEnumerator Scale(Transform objectTrans, Vector2 targetScale, float animationDuration)
        {
            Vector2 startScale = objectTrans.localScale;
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                // Calculate the progress of the animation (a value between 0 and 1)
                float progress = elapsedTime / animationDuration;

                // Interpolate the current scale values towards the targetScale
                Vector2 currentScale = Vector2.Lerp(startScale, targetScale, progress);

                // Apply the new scale
                objectTrans.localScale = currentScale;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final scale is exactly the target scale to avoid floating-point imprecisions
            objectTrans.localScale = targetScale;
        }

        public void SetVisible(bool isVisible)
        {
            part01.Child.GetComponent<SpriteRenderer>().enabled = isVisible;
            part02.Child.GetComponent<SpriteRenderer>().enabled = isVisible;
            circle.enabled = isVisible;
        }
    }
}

