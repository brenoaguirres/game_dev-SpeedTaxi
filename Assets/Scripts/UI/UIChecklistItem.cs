using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpeedTaxi.UI
{
    public class UIChecklistItem : MonoBehaviour
    {
        #region FIELDS
        private string _objectiveId = "";
        #endregion

        #region PROPERTIES
        public string ObjectiveId
        {
            get => _objectiveId;
            set => _objectiveId = value;
        }
        #endregion
        
        #region REFERENCES
        [Header("Settings")]
        [SerializeField] private Image _checkmark;
        [SerializeField] private TextMeshProUGUI _description;
        #endregion

        #region CUSTOM METHODS

        public void SetDescription(string text)
        {
            _description.text = text;
        }

        public void ToggleObjective(bool toggle)
        {
            _checkmark.enabled = toggle;
        }

        #endregion
    }
}