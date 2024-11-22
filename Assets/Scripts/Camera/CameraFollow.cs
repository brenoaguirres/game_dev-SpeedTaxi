using System;
using UnityEngine;
using SpeedTaxi.Inputs;
using UnityEngine.Serialization;

namespace SpeedTaxi.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        #region FIELDS
        [Header("Follow Settings")]
        [Tooltip("The object which the camera should follow.")]
        [SerializeField] private Transform _followObject;
        private Vector3 _initialOffset;
        
        [Tooltip("The offset to wait before following.")] 
        [SerializeField] private Vector3 _followOffset = new Vector3(0, 4f, 2.5f);
        private Vector3 _currentFollowOffset;
        
        [Space(2)]
        [Header("Input")]
        [Tooltip("Player Inputs from which the camera observes if is there any movement to offset.")]
        [SerializeField] private PlayerInputs _input;

        private float _lerpFactor = 0f;
        private float _lastInput = 0f;
        [Tooltip("Speed in which the camera lerps to offset when moving.")]
        [SerializeField] private float _cameraSpeed = 5f;
        #endregion
        
        #region PROPERTIES

        private float LastInput
        {
            set
            {
                if (_lastInput == value) return;
                
                _lastInput = value;
                ResetOffsetLerp();
            }
        }
        
        #endregion
        
        #region UNITY CALLBACKS

        private void Awake()
        {
            _initialOffset = transform.position - _followObject.position;
            _currentFollowOffset = new Vector3(0, 0, 0);
        }

        private void Update()
        {
            _lerpFactor += Mathf.Clamp01(Time.deltaTime / _cameraSpeed);
            
            if (_input.Accelerate > 0.05f)
            {
                LastInput = 1;
                _currentFollowOffset = Vector3.Lerp(_currentFollowOffset, _followOffset, _lerpFactor);
            }
            else if (_input.Accelerate < -0.05f)
            {
                LastInput = -1;
                _currentFollowOffset = Vector3.Lerp(_currentFollowOffset, 
                    new Vector3(_followOffset.x, _followOffset.y, -_followOffset.z * 2f), _lerpFactor);
            }
            else
            {
                LastInput = 0;
                _currentFollowOffset = Vector3.Lerp(_currentFollowOffset, Vector3.zero, _lerpFactor);
            }
            
            transform.position = transform.position = _followObject.position + _initialOffset + _currentFollowOffset;
        }
        #endregion
        
        #region CUSTOM METHODS

        private void ResetOffsetLerp()
        {
            _lerpFactor = 0;
            
        }
        #endregion
    }
}
