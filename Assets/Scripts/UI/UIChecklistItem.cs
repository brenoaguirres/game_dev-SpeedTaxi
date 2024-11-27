using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChecklistItem : MonoBehaviour
{
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
