using SpeedTaxi.CustomerSystem;
using UnityEngine;

namespace SpeedTaxi.Hints
{
    public class DirectionalHint : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private GameObject _currentTarget; // The target object to point to.
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            if (_currentTarget != null)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(_currentTarget.transform.position - transform.position),
                    _rotationSpeed * Time.deltaTime
                    );
            }
        }
        #endregion
    }
}
