using SpeedTaxi.Inputs;
using SpeedTaxi.Vehicle;
using UnityEngine;

namespace SpeedTaxi.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region FIELDS
        private bool _initialized = false;
        #endregion

        #region PROPERTIES
        public PlayerState CurrentState
        {
            get { return _currentState; }
            set {  _currentState = value; }
        }

        public PlayerStateFactory States
        {
            get { return _states; }
            set { _states = value; }
        }

        public PlayerInputs Inputs
        {
            get { return _playerInputs; }
            set { _playerInputs = value; }
        }

        public Rigidbody Rigidbody
        {
            get { return _rigidbody; }
            set { _rigidbody = value; }
        }

        public VehiclePhysics VehiclePhysics
        {
            get { return _vehiclePhysics; }
            set { _vehiclePhysics = value; }
        }
        #endregion

        #region REFERENCES
        [Tooltip("Script that manages player's inputs.")]
        [SerializeField] private PlayerInputs _playerInputs;
        [Tooltip("Player Rigidbody.")]
        [SerializeField] private Rigidbody _rigidbody;
        [Tooltip("Script that manages player's inputs.")]
        [SerializeField] private VehiclePhysics _vehiclePhysics;
        #endregion

        #region STATE PATTERN
        private PlayerState _currentState;
        private PlayerStateFactory _states;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _vehiclePhysics.InitializeVehicle(_rigidbody);
            Initialize();
        }
        private void Update()
        {

            if (!_initialized) return;

            _currentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            if (!_initialized) return;

            _currentState.FixedUpdateStates();
        }
        #endregion

        #region CUSTOM METHODS
        public void Initialize()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();

            _initialized = true;
        }
        #endregion
    }
}