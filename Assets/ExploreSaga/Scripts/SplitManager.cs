using System.Collections;
using UnityEngine;

namespace ExploreSaga
{
    public class SplitManager : MonoBehaviour
    {
        public static SplitManager Instance { get; private set; }

        [Header("References")]
        [SerializeField] private SplitLine horizontalSplitPrefab;
        [SerializeField] private SplitLine VerticalSplitPrefab;


        [Header("Properties")]
        public SplitType splitType;


        private void Awake()
        {
            Instance = this;
        }



        public void CreateSplitLine(Vector2 position, Vector3 hitPointA, Vector3 hitPointB, float timeSpread = 0.3f)
        {
            switch (splitType)
            {
                default: break;
                case SplitType.Horizontal:
                    var horizontalSplitLine = Instantiate(horizontalSplitPrefab, position, Quaternion.identity);
                    horizontalSplitLine.Spread(hitPointA, hitPointB, splitType, timeSpread);
                    StartCoroutine(Utilities.WaitAfter(timeSpread, () =>
                    {
                        horizontalSplitLine.SetVisible(false);
                        Destroy(horizontalSplitLine.gameObject, 0.2f);
                    }));
                    
                    break;
                case SplitType.Vertical:
                    var verticalSplitLine = Instantiate(VerticalSplitPrefab, position, Quaternion.identity);
                    verticalSplitLine.Spread(hitPointA, hitPointB, splitType, timeSpread);
                    StartCoroutine(Utilities.WaitAfter(timeSpread, () =>
                    {
                        verticalSplitLine.SetVisible(false);
                        Destroy(verticalSplitLine.gameObject, 0.2f);
                    }));
                    break;
            }
        }


    }
}

