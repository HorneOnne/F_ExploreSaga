using UnityEngine;
using UnityEngine.UI;

namespace ExploreSaga
{
    public class UIMainMenu : ExploreSagaCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button gameBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button exitBtn;


        private void Start()
        {
            gameBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);            
            });

            settingsBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplaySettingsMenu(true);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            exitBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                // Call Application.Quit() to exit the game.
                Application.Quit();

                // For the Unity Editor, this will not quit the application. It will stop the editor's play mode.
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            });
        }

        private void OnDestroy()
        {
            gameBtn.onClick.RemoveAllListeners();
            settingsBtn.onClick.RemoveAllListeners();
            exitBtn.onClick.RemoveAllListeners();
        }
    }
}
