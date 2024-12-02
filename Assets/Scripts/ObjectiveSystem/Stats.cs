using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace SpeedTaxi.ObjectiveSystem
{
    public class Stats : MonoBehaviour
    {
        #region FIELDS
        private int _maxScore = 0;
        private float _damageCost = 0;
        private int _timeRemaining = 0;
        private int _customersDelivered = 0;
        private List<char> _taxiLetters = new List<char>();
        private int _bearsKilled = 0;
        private bool _secretAreaFound = false;
        private bool _extraObjective1 = false;
        private bool _extraObjective2 = false;
        #endregion
        
        #region PROPERTIES
        public int MaxScore
        {
            get => _maxScore;
            set { _maxScore = value; onScoreUpdated?.Invoke();}
        }

        public float DamageCost
        {
            get => _damageCost;
            set { _damageCost = value; onDamageValueUpdated?.Invoke();}
        }

        public int TimeRemaining
        {
            get => _timeRemaining;
            set => _timeRemaining = value;
        }

        public int CustomersDelivered
        {
            get => _customersDelivered;
            set { _customersDelivered = value; onCustomerDelivered?.Invoke();}
        }

        public List<char> TaxiLetters
        {
            get => _taxiLetters;
            set { _taxiLetters = value; onTaxiLetterFound?.Invoke();}
        }

        public int BearsKilled
        {
            get => _bearsKilled;
            set { _bearsKilled = value; onBearKilled?.Invoke();}
        }

        public bool SecretAreaFound
        {
            get => _secretAreaFound;
            set { _secretAreaFound = value; onSecretAreaFound?.Invoke();}
        }

        public bool ExtraObjective1
        {
            get => _extraObjective1;
            set { _extraObjective1 = value; onExtraObjective1?.Invoke();}
        }

        public bool ExtraObjective2
        {
            get => _extraObjective2;
            set { _extraObjective2 = value; onExtraObjective2?.Invoke();}
        }
        #endregion

        #region UNITY EVENTS
        public UnityEvent onScoreUpdated = new UnityEvent();
        public UnityEvent onDamageValueUpdated = new UnityEvent();
        public UnityEvent onCustomerDelivered = new UnityEvent();
        public UnityEvent onExtraObjective1 = new UnityEvent();
        public UnityEvent onExtraObjective2 = new UnityEvent();
        public UnityEvent onSecretAreaFound = new UnityEvent();
        public UnityEvent onBearKilled = new UnityEvent();
        public UnityEvent onTaxiLetterFound = new UnityEvent();
        #endregion
    }
}
