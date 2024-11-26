using SpeedTaxi.UI;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedTaxi.ScoreSystem
{
    public class Score : MonoBehaviour
    {
        #region CONSTANTS
        private const int FORMATTING_DIGITS = 7;
        #endregion

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

            score = score.PadLeft(FORMATTING_DIGITS, '0');
            _uiLabel.NextText = score;
        }
        #endregion
    }
}
