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
        private bool _handbrakeInput = false;

        // -----> ACCELERATION
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
        private float _inertiaDecelerationFactor = 15f;

        // -----> ROTATION
        // Amount of rotation per second
        private float _steerAngle = 35f;
        
        // -----> BRAKING
        // For storing default steer angle when handbraking
        private float _defaultSteerAngle = 0f;
        // For multiplying steer angle when handbraking
        private float _steerAngleBrakeMultiplier = 2f;
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

        public bool HandbrakeInput
        {
            get { return _handbrakeInput; }
            set { _handbrakeInput = value; }
        }
        #endregion

        #region UNITY CALLBACKS
        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            ResolveHandbrake();
            ResolveEngine();
            ResolveWheels();
        }
        #endregion

        #region CUSTOM METHODS
        public void InitializeVehicle(Rigidbody rigidbody)
        {
            // components
            _vehicleRigidbody = rigidbody;

            // settings
            _defaultSteerAngle = _steerAngle;
        }

        public void ResolveEngine()
        {
            if (HandbrakeInput) return;
            
            if (EngineInput >= 0.05f) // Forward Acceleration
            {
                // Accelerating
                _currentMoveSpeed += (_accelerationFactor * Time.fixedDeltaTime);
                _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                _vehicleRigidbody.linearVelocity = new Vector3(
                    _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                    _vehicleRigidbody.linearVelocity.y, 
                    _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                
            }
            else if (EngineInput <= -0.05f) // Reverse Acceleration
            {
                // Braking
                if (_vehicleRigidbody.linearVelocity.z > 0f)
                {
                    _currentMoveSpeed += (-_brakeForce * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                } // Reverse
                else if (_vehicleRigidbody.linearVelocity.z <= 0f)
                {
                    _currentMoveSpeed += (-_decelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                }
            }
            else // Inertia
            {
                if (_vehicleRigidbody.linearVelocity.z > 0.05f) // When F
                {
                    _currentMoveSpeed += (-_inertiaDecelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                }
                else if (_vehicleRigidbody.linearVelocity.z < -0.05f) // When R
                {
                    _currentMoveSpeed += (_inertiaDecelerationFactor * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                }
                else // Stop
                {
                    _currentMoveSpeed = 0f;
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        0f,
                        _vehicleRigidbody.linearVelocity.y, 
                        0f
                    );
                }
            }
        }

        public void ResolveHandbrake()
        {
            if (HandbrakeInput)
            {
                // drifting
                if (_steerAngle < _defaultSteerAngle + 0.05f && _steerAngle > _defaultSteerAngle + -0.05f)
                    _steerAngle *= _steerAngleBrakeMultiplier;
                
                // when forward motion
                if (_vehicleRigidbody.linearVelocity.z > 0.05f)
                {
                    _currentMoveSpeed += (-_brakeForce * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                }
                else if (_vehicleRigidbody.linearVelocity.z < -0.05) // when rev motion
                {
                    _currentMoveSpeed += (_brakeForce * Time.fixedDeltaTime);
                    _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed, _maxReverseSpeed, _maxMoveSpeed);
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        _vehicleRigidbody.transform.forward.x * _currentMoveSpeed,
                        _vehicleRigidbody.linearVelocity.y, 
                        _vehicleRigidbody.transform.forward.z * _currentMoveSpeed
                    );
                }
                else // when 
                {
                    _currentMoveSpeed = 0f;
                    _vehicleRigidbody.linearVelocity = new Vector3(
                        0f,
                        _vehicleRigidbody.linearVelocity.y, 
                        0f
                    );
                }
            }
            else
            {
                _steerAngle = _defaultSteerAngle;
            }
        }
        
        public void ResolveWheels()
        {
            if (WheelsInput >= 0.05f && _vehicleRigidbody.linearVelocity.magnitude > 0.1f)
            {
                float steerAmount = _steerAngle * Time.fixedDeltaTime;
                Quaternion targetRotation = _vehicleRigidbody.transform.rotation * Quaternion.Euler(0, steerAmount, 0);
                _vehicleRigidbody.MoveRotation(targetRotation);
            }
            else if (WheelsInput <= -0.05f && _vehicleRigidbody.linearVelocity.magnitude > 0.1f)
            {
                float steerAmount = _steerAngle * Time.fixedDeltaTime;
                Quaternion targetRotation = _vehicleRigidbody.transform.rotation * Quaternion.Euler(0, -steerAmount, 0);
                _vehicleRigidbody.MoveRotation(targetRotation);
            }
            else
            {
                _vehicleRigidbody.angularVelocity = Vector3.zero;
            }
        }
        #endregion
    }
}