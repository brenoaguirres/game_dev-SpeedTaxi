using UnityEngine;

namespace SpeedTaxi.Vehicle
{
    public class VehiclePhysics : MonoBehaviour
    {
        #region REFERENCES
        private Rigidbody _vehicleRigidbody;
        #endregion

        #region FIELDS
        [Range(-1f, 1f)]
        private float _engineInput = 0f;
        [Range(-1f, 1f)]
        private float _wheelsInput = 0f;

        // Maximum move speed achieved by vehicle
        private float _maxMoveSpeed = 50f;
        // Maximum move speed in R gear
        private float _maxReverseSpeed = -40f;
        // Current move speed of the vehicle
        [SerializeField] private float _currentMoveSpeed = 0f;
        // Acceleration Factor in units per second
        private float _accelerationFactor = 20f;
        // Deceleration Factor in units per second
        private float _decelerationFactor = 10f;
        // Braking Factor in units per second
        private float _brakeForce = 35f;
        // Inertia Deceleration Factor in units per second
        private float _inertiaDecelerationFactor = 5f;
        #endregion

        #region PROPERTIES
        private Rigidbody VehicleRigidbody
        {
            get { return _vehicleRigidbody; }
        }
        public float EngineInput
        {
            get { return _engineInput; }
            set { _engineInput = value; }
        }

        public float WheelsInput
        {
            get { return _wheelsInput; }
            set { _wheelsInput = value; }
        }
        #endregion

        #region UNITY CALLBACKS
        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            ResolveEngine();
            ResolveWheels();
        }
        #endregion

        #region CUSTOM METHODS
        public void InitializeVehicle(Rigidbody rigidbody)
        {
            _vehicleRigidbody = rigidbody;
        }

        public void ResolveEngine()
        {
            if (EngineInput >= 0.05f) // Forward Acceleration
            {
                // Accelerating
                _currentMoveSpeed += (_accelerationFactor * Time.fixedDeltaTime);
                _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                _vehicleRigidbody.linearVelocity = _vehicleRigidbody.transform.forward * _currentMoveSpeed;
            }
            else if (EngineInput <= -0.05f) // Reverse Acceleration
            {
                // Braking
                if (_vehicleRigidbody.linearVelocity.z > 0f)
                {
                    _currentMoveSpeed += (-_brakeForce * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = _vehicleRigidbody.transform.forward * _currentMoveSpeed;
                } // Reverse
                else if (_vehicleRigidbody.linearVelocity.z <= 0f)
                {
                    _currentMoveSpeed += (-_decelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = _vehicleRigidbody.transform.forward * _currentMoveSpeed;
                }
            }
            else // Inertia
            {
                if (_vehicleRigidbody.linearVelocity.z > 0.05f) // When F
                {
                    _currentMoveSpeed += (-_inertiaDecelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = _vehicleRigidbody.transform.forward * _currentMoveSpeed;
                }
                else if (_vehicleRigidbody.linearVelocity.z < -0.05f) // When R
                {
                    _currentMoveSpeed += (_inertiaDecelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = _vehicleRigidbody.transform.forward * _currentMoveSpeed;
                }
                else // Stop
                {
                    _vehicleRigidbody.linearVelocity = Vector3.zero;
                    _currentMoveSpeed = 0f;
                }
            }
        }

        public void ResolveWheels()
        {
            if (WheelsInput >= 0.05f)
            {

            }
            else if (WheelsInput <= -0.05f)
            {

            }
            else
            {

            }
        }
        #endregion
    }
}