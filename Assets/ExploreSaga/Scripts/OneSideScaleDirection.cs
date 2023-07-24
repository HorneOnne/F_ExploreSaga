using System.Collections;
using UnityEngine;

namespace ExploreSaga
{
    public class OneSideScaleDirection : MonoBehaviour
    {
        [SerializeField] private Transform child;

        public enum Corner
        {
            UpperLeft,
            LowerLeft,
            UpperRight,
            LowerRight,
            Left, 
            Right, 
            Up,
            Down, 
            Center
        }

        public Corner startCorner;


        #region Properties
        public Transform Child { get => child; }
        #endregion

        private void Awake()
        {
            UpdateChildPositionBaseOnCorner();
        }

        private void UpdateChildPositionBaseOnCorner()
        {
            switch (startCorner)
            {
                default: break;
                case Corner.Center:
                    child.transform.localPosition = new Vector3(0f, 0f, 0f);
                    break;
                case Corner.Left:
                    child.transform.localPosition = new Vector3(0.5f, 0f, 0f);
                    break;
                case Corner.Right:
                    child.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
                    break;
                case Corner.Up:
                    child.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                    break;
                case Corner.Down:
                    child.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                    break;
                case Corner.UpperLeft:
                    child.transform.localPosition = new Vector3(0.5f, -0.5f, 0f);
                    break;
                case Corner.LowerLeft:
                    child.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
                    break;
                case Corner.UpperRight:
                    child.transform.localPosition = new Vector3(-0.5f, -0.5f, 0f);
                    break;
                case Corner.LowerRight:
                    child.transform.localPosition = new Vector3(-0.5f, 0.5f, 0f);
                    break;
            }
        }
    }
}
