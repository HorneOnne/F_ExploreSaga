using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExploreSaga
{

    public class UIGameOver : ExploreSagaCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button repeatBtn;
        [SerializeField] private Button menuBtn;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            scoreText.text = GameManager.Instance.GetScore().ToString();

            repeatBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.GameplayScene);               
            });

            menuBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.MainMenuScene);            
            });

        }

        private void OnDestroy()
        {
            repeatBtn.onClick.RemoveAllListeners();
            menuBtn.onClick.RemoveAllListeners();

        }
    }
}
