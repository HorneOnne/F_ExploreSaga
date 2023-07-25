using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExploreSaga
{
    public class UISplitLineDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static event System.Action<Vector2> OnReleaseUISplitLine;

        [SerializeField] private Canvas canvas;
        private RectTransform rectTransform;
        private Vector2 defaultRectPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultRectPosition = rectTransform.anchoredPosition;
        }


        private void OnEnable()
        {
            WallManager.OnCeateRectSuccessful += ResetPosition;
            InputHandler.OnInputOutOfRectSize += ResetPosition;
        }

        private void OnDisable()
        {
            WallManager.OnCeateRectSuccessful -= ResetPosition;
            InputHandler.OnInputOutOfRectSize -= ResetPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound(SoundType.DragDrop, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector3 worldPosition = rectTransform.TransformPoint(rectTransform.pivot);           
            OnReleaseUISplitLine?.Invoke(worldPosition);
            SoundManager.Instance.PlaySound(SoundType.DragDrop, false);
        }

   
        public void ResetPosition()
        {
            rectTransform.anchoredPosition = defaultRectPosition;
        }
    }
}
