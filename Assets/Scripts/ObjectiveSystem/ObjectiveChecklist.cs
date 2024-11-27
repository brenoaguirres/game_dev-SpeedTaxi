using System;
using SpeedTaxi.ObjectiveSystem;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ObjectiveChecklist : MonoBehaviour
{
    #region FIELDS
    [Header("Data Settings")] 
    [SerializeField] private List<ObjectiveData> _data = new();

    [SerializeField] private GameObject _objectiveItemPrefab;

    private List<ObjectiveItem> _objectives = new();
    #endregion
    
    #region EVENTS
    public UnityEvent onAllObjectivesCompleted;
    #endregion
    
    #region UNITY CALLBACKS
    private void Awake()
    {
        InitializeObjectives();
    }

    private void OnDestroy()
    {
        foreach (ObjectiveItem item in _objectives)
        {
            item.onObjectiveCompleted.RemoveListener(ChecklistObjectives);
        }
    }

    #endregion

    #region CUSTOM METHODS
    private void InitializeObjectives()
    {
        foreach (ObjectiveData entry in _data)
        {
            GameObject instance = Instantiate(
                _objectiveItemPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );

            ObjectiveItem item = instance.GetComponent<ObjectiveItem>();
            item.ObjectiveId = entry._id;
            item.ObjectiveStatus = entry._completed;
            _objectives.Add(item);
            item.onObjectiveCompleted.AddListener(ChecklistObjectives);
        }
    }

    private void ChecklistObjectives(string objectiveId)
    {
        foreach (ObjectiveItem item in _objectives)
        {
            if (!item.ObjectiveStatus) return;
        }
        
        onAllObjectivesCompleted?.Invoke();
    }
    #endregion
}
