using System;
using SpeedTaxi.UI;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedTaxi.ObjectiveSystem
{
    public class LevelTimer : MonoBehaviour
    {
        #region FIELDS
        [Header("Settings")] 
        [SerializeField] private float _startingTimer = 120f;
        private float _maxTimer;
        private float _currentTimer;
        [SerializeField] private bool _countdownOnStart = true;
        private bool _paused = true;
        #endregion

        #region PROPERTIES
        public float MaxTimer
        {
            get { return _maxTimer; }
            set
            {
                _maxTimer += value;
            }
        }
        public float CurrentTimer
        {
            get { return _currentTimer; }
        }
        public bool Paused
        {
            get { return _paused; }
            set
            {
                if (value == false)
                {
                    StartTimer();
                }
                else
                {
                    PauseTimer();
                }
            }
        }
        #endregion
        
        #region REFERENCES
        [SerializeField] private UILabel _uiLabel;
        #endregion
        
        #region UNITY EVENTS
        public UnityEvent onStartTimer;
        public UnityEvent onResetTimer;
        public UnityEvent onEndTimer;
        #endregion
        
        #region UNITY CALLBACKS

        public void Start()
        {
            ResetTimer();
            if (_countdownOnStart)
            {
                StartTimer();
            }
        }

        public void Update()
        {
            if (!_paused)
            {
                _currentTimer -= Time.deltaTime;

                _uiLabel.NextText = ((int)_currentTimer).ToString();
                    
                if (_currentTimer <= 0f)
                    EndTimer();
            }
        }

        #endregion
        
        #region CUSTOM METHODS

        public void StartTimer()
        {
            _paused = false;
            onStartTimer?.Invoke();
            
            _uiLabel.NextText = ((int)_currentTimer).ToString();
        }
        
        public void PauseTimer()
        {
            _paused = true;
        }

        public void ResetTimer()
        {
            onResetTimer?.Invoke();
            _maxTimer = _startingTimer;
            _currentTimer = _maxTimer;
        }

        public void EndTimer()
        {
            PauseTimer();
            onEndTimer?.Invoke();
        }
        #endregion
    }
}