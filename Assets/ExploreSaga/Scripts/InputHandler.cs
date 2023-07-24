using UnityEngine;
using System.Collections;

namespace ExploreSaga
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }
        public static event System.Action OnInputOutOfRectSize;
       

        [Header("Properties")]
        [SerializeField] private LayerMask wallLayer;

        // Cached
        private SplitManager splitManager;
        private WallManager wallManager;


        private void OnEnable()
        {
            UISplitLineDrag.OnReleaseUISplitLine += FireSplitLine;
        }

        private void OnDisable()
        {
            UISplitLineDrag.OnReleaseUISplitLine -= FireSplitLine;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            splitManager = SplitManager.Instance;   
            wallManager = WallManager.Instance;
        }


        private void FireSplitLine(Vector2 position)
        {
            
            if (Utilities.IsPointInsideRectangle(position, wallManager.UpperLeft.position, wallManager.UpperRight.position,
                wallManager.LowerRight.position, wallManager.LowerLeft.position, WallManager.Instance.Offset))
            {
                // Inside 

                switch (splitManager.splitType)
                {
                    default: break;
                    case SplitType.Horizontal:
                        RaycastHit2D leftHit = Physics2D.Raycast(position, Vector2.left, 100, wallLayer);
                        RaycastHit2D rightHit = Physics2D.Raycast(position, Vector2.right, 100, wallLayer);
                        if (leftHit.collider != null && rightHit.collider != null)
                        {
                            Debug.DrawLine(position, leftHit.point, Color.red, 1f);
                            Debug.DrawLine(position, rightHit.point, Color.blue, 1f);
                            splitManager.CreateSplitLine(position, leftHit.point, rightHit.point);
                        }
                        break;
                    case SplitType.Vertical:
                        RaycastHit2D upHit = Physics2D.Raycast(position, Vector2.up, 100, wallLayer);
                        RaycastHit2D downHit = Physics2D.Raycast(position, Vector2.down, 100, wallLayer);
                        if (upHit.collider != null && downHit.collider != null)
                        {
                            Debug.DrawLine(position, upHit.point, Color.red, 1f);
                            Debug.DrawLine(position, downHit.point, Color.blue, 1f);
                            splitManager.CreateSplitLine(position, upHit.point, downHit.point);
                        }
                        break;
                }
            }
            else
            {
                // OutSide
                OnInputOutOfRectSize?.Invoke();
            }
        }        
    }
}

