using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "Scriptable Objects/ObjectiveData")]
public class ObjectiveData : ScriptableObject
{
    #region FIELDS
    [Header("Data")]
    public bool _completed = false;
    public string _description = "";
    #endregion
}
