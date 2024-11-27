using UnityEngine;

namespace SpeedTaxi.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "ObjectiveData", menuName = "Scriptable Objects/ObjectiveData")]
    public class ObjectiveData : ScriptableObject
    {
        #region FIELDS

        [Header("Data")] public string _id = "";
        public bool _completed = false;
        public string _description = "";

        #endregion
    }
}
