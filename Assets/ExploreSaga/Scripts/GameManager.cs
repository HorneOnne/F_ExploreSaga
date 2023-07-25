using System.Collections.Generic;
using UnityEngine;

namespace ExploreSaga
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private int currentLevel = 1;


        #region Properties
        public int Level { get { return currentLevel; } }
        #endregion


        // Data
        [SerializeField] private int score = 0;
        [SerializeField] private int bestScore = 0;


        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;
        }

        private void Start()
        {           
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);          
        }

  

        public void PreviousLevel()
        {
            currentLevel--;
            if (currentLevel < 1)
                currentLevel = 1;
        }


        #region SCORE & BEST
        public void ScoreUp()
        {
            score++;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetBestScore()
        {
            if(bestScore < score)
                bestScore  = score;

            return bestScore;
        }

        public void ResetScore()
        {
            score = 0;
        }
        #endregion
    }

}
