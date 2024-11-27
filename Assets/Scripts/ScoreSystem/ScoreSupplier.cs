using SpeedTaxi.Managers;
using UnityEngine;

namespace SpeedTaxi.ScoreSystem
{
    public class ScoreSupplier : MonoBehaviour, IScoreInitializer
    {
        #region FIELDS
        [Header("Score Config")]
        [Tooltip("Value to multiply by base score quantity.")]
        [SerializeField] private int _scoreMultiplier = 0;
        #endregion

        #region PROPERTIES
        public int ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
        #endregion

        #region REFERENCES
        [Tooltip("Score Component")]
        [SerializeField] private Score _score;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            if (_score == null)
            {
                Debug.LogError($"Score Component reference not assigned on '{gameObject.name}' ScoreSupplier.");
            }
                
        }
        #endregion

        #region CUSTOM METHODS
        public void GenerateScore()
        {
            _score.AddScore(_scoreMultiplier * GlobalConstants.BASE_SCORE);
        }

        public void InitializeScore(int scoreMultiplier)
        {
            _scoreMultiplier = scoreMultiplier;
        }
        #endregion
    }
}
