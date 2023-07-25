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


        [Header("Sprites")]
        [SerializeField] private Sprite unmuteSrite;
        [SerializeField] private Sprite muteSprite;
        

        // Cached
        private SoundManager soundManager;

   
        private void Start()
        {
            soundManager = SoundManager.Instance;

            UpdateMusicUI();
            UpdateSoundFXUI();

            soundBtn.onClick.AddListener(() =>
            {
                soundManager.PlaySound(SoundType.Button, false);
                ToggleSFX();
            });

            musicBtn.onClick.AddListener(() =>
            {
                soundManager.PlaySound(SoundType.Button, false);
                ToggleMusic();
            });

            backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);

                soundManager.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            soundBtn.onClick.RemoveAllListeners();
            musicBtn.onClick.RemoveAllListeners();
            backBtn.onClick.RemoveAllListeners();     
        }
        private void ToggleMusic(bool updateUI = true)
        {
            soundManager.MuteBackground(soundManager.isMusicActive);
            soundManager.isMusicActive = !soundManager.isMusicActive;
            
            if (updateUI)
                UpdateMusicUI();
        }

        private void ToggleSFX(bool updateUI = true)
        {
            soundManager.MuteSoundFX(soundManager.isSoundFXActive);
            soundManager.isSoundFXActive = !soundManager.isSoundFXActive;
           
            if (updateUI)
                UpdateSoundFXUI();
        }

        private void UpdateMusicUI()
        {
            if(soundManager.isMusicActive)
            {
                musicBtn.image.sprite = unmuteSrite;
            }
            else
            {
                musicBtn.image.sprite = muteSprite;
            }
        }

        private void UpdateSoundFXUI()
        {
            if (soundManager.isSoundFXActive)
            {
                soundBtn.image.sprite = unmuteSrite;
            }
            else
            {
                soundBtn.image.sprite = muteSprite;
            }
        }
    }
}
