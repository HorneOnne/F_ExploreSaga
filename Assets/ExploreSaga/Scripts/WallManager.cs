using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ExploreSaga
{
    public class WallManager : MonoBehaviour
    {
        public static WallManager Instance { get; private set; }

        [Header("Walls")]
        [SerializeField] private Wall upperLeft;
        [SerializeField] private Wall upperRight;
        [SerializeField] private Wall lowerLeft;
        [SerializeField] private Wall lowerRight;

        [Header("InsideBackground")]
        [SerializeField] private GameObject insideBackgroundPrefab;
        private GameObject insideBackground;


        // Cached
        private float distanceH;
        private float distanceV;


        #region Properties
        public Transform UpperLeft { get => upperLeft.transform; }
        public Transform UpperRight { get => upperRight.transform; }
        public Transform LowerLeft { get => lowerLeft.transform; }
        public Transform LowerRight { get => lowerRight.transform; }
        #endregion


        private void Awake()
        {
            Instance = this;
            CreateRect();
        }

        private void OnEnable()
        {
            SplitLine.OnLineSpreadCompleted += RecreateRect;
        }

        private void OnDisable()
        {
            SplitLine.OnLineSpreadCompleted -= RecreateRect;
        }


        private void CreateRect()
        {
            upperLeft.ResetScale();
            upperRight.ResetScale();
            lowerLeft.ResetScale();
            lowerRight.ResetScale();


            distanceH = Vector2.Distance(upperLeft.transform.position, upperRight.transform.position);
            upperLeft.ScaleX(distanceH);
            lowerRight.ScaleX(distanceH);

            distanceV = Vector2.Distance(upperLeft.transform.position, lowerLeft.transform.position);
            upperRight.ScaleY(distanceV);
            lowerLeft.ScaleY(distanceV);


            // Background inside RECT
            var centerPoint = Utilities.CalculateRectangleCenter(upperLeft.transform.position, upperRight.transform.position,
                    lowerRight.transform.position, lowerLeft.transform.position);
            if (insideBackground == null)
                insideBackground = Instantiate(insideBackgroundPrefab);
            insideBackground.transform.position = centerPoint;
            insideBackground.transform.localScale = new Vector3(distanceH, distanceV, 1);
        }


        private void RecreateRect(SplitType splitType, Vector2 hitPointA, Vector2 hitPointB)
        {
            switch (splitType)
            {
                default: break;
                case SplitType.Horizontal:
                    if(Ball.Instance.transform.position.y < hitPointA.y)
                    {
                        //Ball Below Line
                        upperLeft.transform.position = new Vector3(upperLeft.transform.position.x, hitPointA.y, upperLeft.transform.position.z);
                        upperRight.transform.position = new Vector3(upperRight.transform.position.x, hitPointA.y, upperRight.transform.position.z);
                        CreateRect();
                    }
                    else
                    {
                        // Ball Above Line
                        lowerLeft.transform.position = new Vector3(lowerLeft.transform.position.x, hitPointA.y, lowerLeft.transform.position.z);
                        lowerRight.transform.position = new Vector3(lowerRight.transform.position.x, hitPointA.y, lowerRight.transform.position.z);
                        CreateRect();
                    }
                    break;
                case SplitType.Vertical:
                    if (Ball.Instance.transform.position.x < hitPointA.x)
                    {
                        //Ball Left Side Line
                        upperRight.transform.position = new Vector3(hitPointA.x, upperRight.transform.position.y, upperRight.transform.position.z);
                        lowerRight.transform.position = new Vector3(hitPointA.x, lowerRight.transform.position.y, lowerRight.transform.position.z);
                        CreateRect();
                    }
                    else
                    {
                        // Ball Right Side Line
                        upperLeft.transform.position = new Vector3(hitPointA.x, upperLeft.transform.position.y, upperLeft.transform.position.z);
                        lowerLeft.transform.position = new Vector3(hitPointA.x, lowerLeft.transform.position.y, lowerLeft.transform.position.z);
                        CreateRect();
                    }
                    break;
            }
        }
    }
}
