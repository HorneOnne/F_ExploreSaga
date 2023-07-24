using UnityEngine;
using UnityEngine.UI;

namespace ExploreSaga
{
    public class UISettings : ExploreSagaCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button soundBtn;
        [SerializeField] private Button musicBtn;
        [SerializeField] private Button backBtn;
  

        private void Start()
        {
            soundBtn.onClick.AddListener(() =>
            {

            });

            musicBtn.onClick.AddListener(() =>
            {

            });

            backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });
        }

        private void OnDestroy()
        {
            soundBtn.onClick.RemoveAllListeners();
            musicBtn.onClick.RemoveAllListeners();
            backBtn.onClick.RemoveAllListeners();     
        }
    }
}
