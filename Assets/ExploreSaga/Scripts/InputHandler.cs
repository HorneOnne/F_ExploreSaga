using UnityEngine;
using System.Collections;

namespace ExploreSaga
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }

        [Header("Properties")]
        [SerializeField] private LayerMask wallLayer;

        // Cached
        private SplitManager splitManager;
        private WallManager wallManager;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            splitManager = SplitManager.Instance;   
            wallManager = WallManager.Instance;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float offset = -3f;
                if (Utilities.IsPointInsideRectangle(mousePosition, wallManager.UpperLeft.position, wallManager.UpperRight.position,
                    wallManager.LowerRight.position, wallManager.LowerLeft.position, offset))
                {                 
                    //Debug.Log($"Inside");

                    switch(splitManager.splitType)
                    {
                        default: break;
                        case SplitType.Horizontal:
                            RaycastHit2D leftHit = Physics2D.Raycast(mousePosition, Vector2.left, 100, wallLayer);
                            RaycastHit2D rightHit = Physics2D.Raycast(mousePosition, Vector2.right, 100, wallLayer);
                            if(leftHit.collider != null && rightHit.collider != null)
                            {
                                Debug.DrawLine(mousePosition, leftHit.point, Color.red, 1f);
                                Debug.DrawLine(mousePosition, rightHit.point, Color.blue, 1f);                      
                                splitManager.CreateSplitLine(mousePosition, leftHit.point, rightHit.point);
                            }
                            break;
                        case SplitType.Vertical:
                            RaycastHit2D upHit = Physics2D.Raycast(mousePosition, Vector2.up, 100, wallLayer);
                            RaycastHit2D downHit = Physics2D.Raycast(mousePosition, Vector2.down, 100, wallLayer);
                            if (upHit.collider != null && downHit.collider != null)
                            {
                                Debug.DrawLine(mousePosition, upHit.point, Color.red, 1f);
                                Debug.DrawLine(mousePosition, downHit.point, Color.blue, 1f);
                                splitManager.CreateSplitLine(mousePosition, upHit.point, downHit.point);
                            }
                            break;
                    }
                    

                    /*if (leftHit.collider != null)
                    {
                        Debug.Log($"Hit wall at {leftHit.point}");
                        splitManager.CreateSplitLine(mousePosition); 
                    }*/

        
                }
                else
                {
                    //Debug.Log("OutSide");
                }


                
            }
        }



        
    }
}

