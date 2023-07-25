using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExploreSaga
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnPlaying;
        public static event System.Action OnWin;
        public static event System.Action OnGameOver;

        public enum GameState
        {
            LOADING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
        }


        [Header("Properties")]
        public GameState currentState;
        [SerializeField] private float waitTimeBeforePlaying = 0.5f;



        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            currentState = GameState.PLAYING;

            // Reset score
            GameManager.Instance.ResetScore();
        }

     

        public void ChangeGameState(GameState state)
        {
            currentState = state;
            OnStateChanged?.Invoke();
        }

        private void SwitchState()
        {
            switch(currentState)
            {
                default: break;
                case GameState.PLAYING:
                    OnPlaying?.Invoke();
                    break;
                case GameState.WIN:               
                    GameManager.Instance.ScoreUp();
                    StartCoroutine(Utilities.WaitAfter(1f, () =>
                    {
                        ChangeGameState(GameState.PLAYING);
                    }));
                    SoundManager.Instance.PlaySound(SoundType.Win, false);
                    OnWin?.Invoke();
                    break;
                case GameState.GAMEOVER:
                    CameraShake.Instance.Shake();
                    SoundManager.Instance.PlaySound(SoundType.Destroyed, false);
                    Time.timeScale = 0.0f;

                    StartCoroutine(Utilities.WaitAfterRealtime(1.0f, () =>
                    {
                        Time.timeScale = 1.0f;
                        Loader.Load(Loader.Scene.GameOverScene);
                        SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                    }));
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
    }       
}
