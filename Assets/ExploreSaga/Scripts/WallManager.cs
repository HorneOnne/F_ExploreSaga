using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace ExploreSaga
{
    public class WallManager : MonoBehaviour
    {
        public static WallManager Instance { get; private set; }
        public static event System.Action OnCeateRectSuccessful;

        [Header("Walls")]
        [SerializeField] private Wall upperLeft;
        [SerializeField] private Wall upperRight;
        [SerializeField] private Wall lowerLeft;
        [SerializeField] private Wall lowerRight;

        [Header("InsideBackground")]
        [SerializeField] private GameObject insideBackgroundPrefab;
        private GameObject insideBackground;


        // Cached
        [SerializeField] private Vector2 originCenter;
        private float distanceH;
        private float distanceV;
        private float winArea = 650;
        private float minSizeToggleWall = 25;

        private const float MAX_X = 24;
        private const float MIN_X = -24;
        private const float MAX_Y = 40;
        private const float MIN_Y = -40;



        #region Properties
        public Transform UpperLeft { get => upperLeft.transform; }
        public Transform UpperRight { get => upperRight.transform; }
        public Transform LowerLeft { get => lowerLeft.transform; }
        public Transform LowerRight { get => lowerRight.transform; }
        public float Offset { get; set; } = -3f;
        #endregion


        private void Awake()
        {
            Instance = this;
            CreateWalls();
        }

        private void OnEnable()
        {
            SplitLine.OnLineSpreadCompleted += RecreateWalls;
            GamePlayManager.OnWin += SetWinState;
        }

        private void OnDisable()
        {
            SplitLine.OnLineSpreadCompleted -= RecreateWalls;
            GamePlayManager.OnWin -= SetWinState;
        }


        private void CreateWalls()
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

            if (distanceH < minSizeToggleWall || distanceV < minSizeToggleWall)
                SetWallsVisible(true);


            OnCeateRectSuccessful?.Invoke();
        }


        private void RecreateWalls(SplitType splitType, Vector2 hitPointA, Vector2 hitPointB)
        {
            switch (splitType)
            {
                default: break;
                case SplitType.Horizontal:
                    if (Ball.Instance.transform.position.y < hitPointA.y)
                    {
                        //Ball Below Line
                        upperLeft.transform.position = new Vector3(upperLeft.transform.position.x, hitPointA.y, upperLeft.transform.position.z);
                        upperRight.transform.position = new Vector3(upperRight.transform.position.x, hitPointA.y, upperRight.transform.position.z);
                        CreateWalls();
                    }
                    else
                    {
                        // Ball Above Line
                        lowerLeft.transform.position = new Vector3(lowerLeft.transform.position.x, hitPointA.y, lowerLeft.transform.position.z);
                        lowerRight.transform.position = new Vector3(lowerRight.transform.position.x, hitPointA.y, lowerRight.transform.position.z);
                        CreateWalls();
                    }
                    break;
                case SplitType.Vertical:
                    if (Ball.Instance.transform.position.x < hitPointA.x)
                    {
                        //Ball Left Side Line
                        upperRight.transform.position = new Vector3(hitPointA.x, upperRight.transform.position.y, upperRight.transform.position.z);
                        lowerRight.transform.position = new Vector3(hitPointA.x, lowerRight.transform.position.y, lowerRight.transform.position.z);
                        CreateWalls();
                    }
                    else
                    {
                        // Ball Right Side Line
                        upperLeft.transform.position = new Vector3(hitPointA.x, upperLeft.transform.position.y, upperLeft.transform.position.z);
                        lowerLeft.transform.position = new Vector3(hitPointA.x, lowerLeft.transform.position.y, lowerLeft.transform.position.z);
                        CreateWalls();
                    }
                    break;
            }

            float differential = CalculateDifferential();
            if (differential < 40)
            {
                bool canWin = CheckConditionCanWin();
                if (canWin)
                    GamePlayManager.Instance.ChangeGameState(GamePlayManager.GameState.WIN);
            }

        }

        private bool CheckConditionCanWin()
        {
            float rectArea = Utilities.CalculateRectangleArea(upperLeft.transform.position, upperRight.transform.position,
                    lowerRight.transform.position, lowerLeft.transform.position, Offset);
            if (rectArea < winArea)
            {
                return true;
            }
            return false;
        }


        private void SetWallsVisible(bool isVisible)
        {
            upperLeft.SetVisible(isVisible);
            upperRight.SetVisible(isVisible);
            lowerLeft.SetVisible(isVisible);
            lowerRight.SetVisible(isVisible);

        }


        private void SetWinState()
        {
            SetWallsVisible(false);
            MoveWallsToCenter();
            ScaleWalls();
            CreateWalls();          
        }

        private void MoveWallsToCenter()
        {
            List<Transform> points = new List<Transform>();
            points.Add(lowerLeft.transform);
            points.Add(upperLeft.transform);
            points.Add(lowerRight.transform);
            points.Add(upperRight.transform);

            // Calculate the current center of the rectangle
            Vector2 currentCenter = originCenter;
            foreach (Transform point in points)
            {
                currentCenter += (Vector2)point.position;
            }
            currentCenter /= points.Count;

            // Calculate the offset to move the rectangle to the desired center position
            Vector2 offset = originCenter - currentCenter;

            // Apply the offset to move all four points to the centered position
            foreach (Transform point in points)
            {
                point.position += (Vector3)offset;
            }
        }

        private void ScaleWalls()
        {
            float scaleX = Mathf.Abs(MIN_X - upperLeft.transform.position.x);
            float scaleY = Mathf.Abs(MIN_Y - upperLeft.transform.position.y);
            float minValue = scaleX < scaleY ? scaleX : scaleY;

            upperLeft.transform.position += new Vector3(-minValue, minValue, 0f);
            upperRight.transform.position += new Vector3(minValue, minValue, 0f);
            lowerLeft.transform.position += new Vector3(-minValue, -minValue, 0f);
            lowerRight.transform.position += new Vector3(minValue, -minValue, 0f);
        }


        private float CalculateDifferential()
        {

            return Mathf.Abs(distanceH - distanceV);
        }
    }
}
