using TMPro;
using UnityEngine;

namespace SpeedTaxi.UI
{
    public class UILabel: MonoBehaviour
    {
        #region FIELDS

        [Header("Settings")] [Tooltip("Text Mesh Pro containing the text box to change text.")] [SerializeField]
        private TextMeshProUGUI _textMesh;

        [Tooltip("Text to insert in text box.")] [SerializeField]
        private string _nextText;

        #endregion

        #region PROPERTIES

        public string NextText
        {
            get => _nextText;
            set
            {
                _nextText = value;
                _textMesh.text = value;
            }
        }

        #endregion
    }
}