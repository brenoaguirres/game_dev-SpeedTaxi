using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpeedTaxi.Inputs
{
    public class PlayerInputs : MonoBehaviour
    {
        #region FIELDS
        
        private PlayerInput _playerInput;
        private PlayerInputActions _inputActions;
        
        private float _accelerate;
        private float _steer;
        private bool _handbrake;
        private bool _skill;

        #endregion

        #region PROPERTIES

        public float Accelerate => _accelerate;
        public float Steer => _steer;
        public bool Handbrake => _handbrake;
        public bool Skill => _skill;
        
        #endregion
        
        #region UNITY CALLBACKS

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            
            _inputActions.Enable();

            _inputActions.Car.Accelerate.started += ctx => _accelerate = ctx.ReadValue<float>();
            _inputActions.Car.Steer.started += ctx => _steer = ctx.ReadValue<float>();
            _inputActions.Car.Handbrake.started += ctx => _handbrake = ctx.ReadValueAsButton();
            _inputActions.Car.Skill.started += ctx => _skill = ctx.ReadValueAsButton();
            
            _inputActions.Car.Accelerate.canceled += ctx => _accelerate = ctx.ReadValue<float>();
            _inputActions.Car.Steer.canceled += ctx => _steer = ctx.ReadValue<float>();
            _inputActions.Car.Handbrake.canceled += ctx => _handbrake = ctx.ReadValueAsButton();
            _inputActions.Car.Skill.canceled += ctx => _skill = ctx.ReadValueAsButton();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
        }
        #endregion
    }
}