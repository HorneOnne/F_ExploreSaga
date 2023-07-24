using UnityEngine;


namespace ExploreSaga
{
    public class Ball : MonoBehaviour
    {
        public static Ball Instance { get; private set; }

        [SerializeField] private LayerMask lineLayer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
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
                Debug.Log("Gameover");
                rb.velocity = Vector2.zero;
            }
        }
    }
}
