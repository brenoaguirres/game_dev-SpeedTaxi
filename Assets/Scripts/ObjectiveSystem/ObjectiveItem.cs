using UnityEngine;
using SpeedTaxi.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace SpeedTaxi.ObjectiveSystem
{
    public class ObjectiveItem : MonoBehaviour
    {
        #region FIELDS
        [Header("Status")] 
        [SerializeField] private ObjectiveData _myData;
        private bool _objectiveStatus = false;
        private string _objectiveId = "";
        #endregion

        #region PROPERTIES
        public bool ObjectiveStatus
        {
            get => _objectiveStatus;
            set
            {
                _objectiveStatus = value;
                if (_uiObjective != null)
                    _uiObjective.ToggleObjective(value);
                if (value)
                    onObjectiveCompleted?.Invoke(_objectiveId);
            }
        }

        public string ObjectiveId
        {
            get => _objectiveId;
            set
            {
                _objectiveId = value;
                if (_uiObjective != null)
                    _uiObjective.ObjectiveId = value;
            }
        }
        #endregion
        
        #region REFERENCES
        private UIChecklistItem _uiObjective;
        #endregion
        
        #region EVENTS
        public UnityEvent<string> onObjectiveCompleted;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            List<UIChecklistItem> uiObjectives = FindObjectsByType<UIChecklistItem>(FindObjectsSortMode.None).ToList();
            foreach (var obj in uiObjectives)
            {
                if (obj.ObjectiveId == _objectiveId)
                {
                    _uiObjective = obj;
                    break;
                }
            }
        }
        #endregion
        
        
    }
}
