using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExploreSaga
{
    public class UIGameplay : ExploreSagaCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button backBtn;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI menuBtn;

        [Header("Gameplay")]
        [SerializeField] private UISplitLineDrag vDrag;
        [SerializeField] private UISplitLineDrag hDrag;


        private void OnEnable()
        {
            SplitManager.OnSplitTypeChanged += ChangeDragType;
        }

        private void OnDisable()
        {
            SplitManager.OnSplitTypeChanged -= ChangeDragType;
        }

        private void Start()
        {
            backBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });

        }

        private void OnDestroy()
        {
            backBtn.onClick.RemoveAllListeners();
        }

        private void ChangeDragType()
        {
            switch(SplitManager.Instance.splitType)
            {
                default: break;
                case SplitType.Horizontal:
                    vDrag.gameObject.SetActive(false);
                    hDrag.gameObject.SetActive(true);
                    break;
                case SplitType.Vertical:
                    hDrag.gameObject.SetActive(false);
                    vDrag.gameObject.SetActive(true);                
                    break;
            }
        }
    }
}
