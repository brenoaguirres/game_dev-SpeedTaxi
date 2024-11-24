using SpeedTaxi.CustomerSystem;
using UnityEngine;

namespace SpeedTaxi.Hints
{
    public class DirectionalHint : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject _fromPosition; // The object indicating the starting position.
        [SerializeField] private GameObject _currentTarget; // The target object to point to.
        [SerializeField] private UnityEngine.Camera _camera; // Reference to the camera.
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            if (_currentTarget != null && _fromPosition != null && _camera != null)
            {
                // Calculate direction vector in world space
                Vector3 worldDirection = _currentTarget.transform.position - _fromPosition.transform.position;

                // Transform the worldDirection into the camera's local space
                Vector3 localDirection = _camera.transform.InverseTransformDirection(worldDirection);

                // Debug: Log the local direction
                // Debug.Log($"Local Direction: {localDirection}");

                // Calculate the angle for rotation in the camera's local 2D plane (XZ)
                float angle = Mathf.Atan2(localDirection.z, localDirection.x) * Mathf.Rad2Deg;

                // Apply rotation in the camera's local space
                transform.localRotation = Quaternion.Euler(0, 0, angle);
            }
        }
        #endregion
    }
}
