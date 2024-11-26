using SpeedTaxi.UI;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedTaxi.ScoreSystem
{
    public class Score : MonoBehaviour
    {
        #region FIELDS
        private int _totalScore = 0;
        #endregion

        #region PROPERTIES
        public int TotalScore { 
            get => _totalScore;
            set 
            {
                _totalScore = value;

            }
        }
        #endregion

        #region REFERENCES
        [Header("UI")]
        public UILabel _uiLabel;
        #endregion

        #region EVENTS
        public UnityEvent onScoreChange;
        #endregion

        #region CUSTOM METHODS
        public void AddScore(int value)
        {
            TotalScore += value;
            onScoreChange?.Invoke();

            UpdateUI();
        }

        public void UpdateUI()
        {
            string score = TotalScore.ToString();

            int loopIterations = 7;
            int counter = 1;
            int zeroes = 7;
            for (int i = loopIterations; i > 0; i--)
            {
                counter = 1 * (int)(Mathf.Pow(10, i));
                if (TotalScore / counter > 0)
                    zeroes--;
                else
                    break;
            }

            for (int i = 0; i < zeroes; i++)
            {
                score = "0" + score;
                Debug.Log(score);
            }

            _uiLabel.NextText = score.ToString();
        }
        #endregion
    }
}
