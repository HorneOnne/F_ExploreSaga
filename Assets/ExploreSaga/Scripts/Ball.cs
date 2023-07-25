using UnityEngine;


namespace ExploreSaga
{
    public class Ball : MonoBehaviour
    {
        public static Ball Instance { get; private set; }

        [SerializeField] private LayerMask lineLayer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D circleCollider2D;
        [SerializeField] private float speed;



        // Cached
        private WallManager wallManager;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GamePlayManager.OnWin += SetWinState;
            GamePlayManager.OnPlaying += SetPlayingState;
        }

        private void OnDisable()
        {
            GamePlayManager.OnWin -= SetWinState;
            GamePlayManager.OnPlaying -= SetPlayingState;
        }

        private void Start()
        {
            wallManager = WallManager.Instance;
            Launch();
        }

        private void Launch()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            rb.velocity = new Vector2(x, y).normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((lineLayer.value & 1 << collision.gameObject.layer) != 0)
            {
                rb.velocity = Vector2.zero;
                GamePlayManager.Instance.ChangeGameState(GamePlayManager.GameState.GAMEOVER);
            }
        }

    
        private void SetEnableCollider(bool isActive)
        {
            circleCollider2D.enabled = isActive;
        }
        
        private void SetWinState()
        {
            rb.AddForce(rb.velocity * 10f, ForceMode2D.Impulse);
            SetEnableCollider(false);
        }

        private void SetPlayingState()
        {
            rb.velocity = Vector2.zero;
          
            var randomInsideRect = Utilities.GetRandomPositionInRectangle(wallManager.UpperLeft.transform.position, wallManager.UpperRight.transform.position,
                    wallManager.LowerRight.transform.position, wallManager.LowerLeft.transform.position, -wallManager.Offset);
              
            SetEnableCollider(true);
            transform.position = randomInsideRect;

            Launch();
        }

    }
}
