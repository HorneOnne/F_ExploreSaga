using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExploreSaga
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnWin;

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

                    break;
                case GameState.WIN:
                    OnWin?.Invoke();
                    break;
                case GameState.GAMEOVER:
     
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
    }       
}
