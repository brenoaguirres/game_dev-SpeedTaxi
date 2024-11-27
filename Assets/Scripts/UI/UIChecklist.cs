using System;
using UnityEngine;
using System.Collections.Generic;
using SpeedTaxi.ObjectiveSystem;

namespace SpeedTaxi.UI
{
    public class UIChecklist : MonoBehaviour
    {
        #region REFERENCES

        [Header("Checklist Settings")] [Tooltip("Data on objectives to fill for the current level.")] [SerializeField]
        private List<ObjectiveData> _data = new();

        [Tooltip("Prefab to spawn checklist items.")] [SerializeField]
        private GameObject _uiChecklistItemPrefab;

        private List<UIChecklistItem> _checklistItems = new();

        #endregion

        #region UNITY CALLBACKS

        private void Awake()
        {
            InitializeUIData();
        }

        #endregion

        #region CUSTOM METHODS

        private void InitializeUIData()
        {
            foreach (ObjectiveData entry in _data)
            {
                GameObject instance = Instantiate(
                    _uiChecklistItemPrefab,
                    transform.position,
                    Quaternion.identity,
                    transform
                );

                UIChecklistItem item = instance.GetComponent<UIChecklistItem>();
                item.ObjectiveId = entry._id;
                item.ToggleObjective(false);
                item.SetDescription(entry._description);
            }
        }

        #endregion
    }
}