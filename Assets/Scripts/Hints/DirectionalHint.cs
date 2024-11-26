using SpeedTaxi.CustomerSystem;
using UnityEngine;

namespace SpeedTaxi.Hints
{
    public class DirectionalHint : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private Transform _pivotObject; // The object to consider the starting point when getting the direction; e.g. The Player Object.
        [SerializeField] private Transform _currentTarget; // The target object to point to.
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            if (_currentTarget != null)
            {
                Vector3 direction = (_currentTarget.position - _pivotObject.position).normalized;
                float angle = Vector3.SignedAngle(direction, -_pivotObject.forward, Vector3.up);
                transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            }
        }
        #endregion
    }
}
