using UnityEngine;

namespace ExploreSaga
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private OneSideScaleDirection oneSideScaleDirection;

        public void ScaleX(float scaleFactorX)
        {
            // Apply the new scale to the object's localScale only on the X-axis.
            Vector3 newScale = transform.localScale;
            newScale.x *= scaleFactorX;
            transform.localScale = newScale;
        }

        public void ScaleY(float scaleFactorY)
        {
            // Apply the new scale to the object's localScale only on the Y-axis.
            Vector3 newScale = transform.localScale;
            newScale.y *= scaleFactorY;
            transform.localScale = newScale;
        }

        public void ResetScale()
        {
            transform.localScale = Vector3.one; 
        }
    }
}
