using System;
using SpeedTaxi.Utils;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

namespace SpeedTaxi.CustomerSystem
{
    public enum State
    {
        INACTIVE,
        WAITING,
        ENTERING,
        RIDING,
        PAYMENT,
        EXITING,
        FINISH
    }

    public class Customer : MonoBehaviour
    {
        #region FIELDS
        [Header("Customer Behavior")] 
        [SerializeField] private float _moveSpeed = 10f;

        [FormerlySerializedAs("_collectArea")]
        [Space(2)] 
        [Header("Control")] 
        [SerializeField] private CollisionNotifier _rideStartPosition;
        [SerializeField] private CollisionNotifier _rideFinishPosition;
        [SerializeField] private MeshRenderer _arrowHint;
        [SerializeField] private Transform _stopPosition;
        
        private Rigidbody _rigidbody;
        private GameObject _player;
        private TransformFollow _transformFollow;
        
        [Space(2)]
        [Header("Character")]
        [SerializeField] private BoxCollider _characterCollider;
        [SerializeField] private MeshRenderer _characterMesh;
        
        [Space(2)]
        [Header("State")]
        [SerializeField] private State _customerState = State.INACTIVE;
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            InitializeCustomer();
        }

        private void OnDisable()
        {
            DisableCustomer();
        }

        private void FixedUpdate()
        {
            switch (_customerState)
            {
                case State.ENTERING:
                    UpdateEntering();
                    break;
                case State.EXITING:
                    UpdateExiting();
                    break;
                case State.FINISH:
                    UpdateFinish();
                    break;
                default:
                    Debug.Log("Unaccounted state reached.");
                    break;
            }
        }
        #endregion

        #region CUSTOM METHODS
        // ==============
        // init / disable
        // ==============
        public void InitializeCustomer()
        {
            _customerState = State.WAITING;

            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            
            _rideStartPosition.onTEnter.AddListener(OnEnterStartArea);
            _rideFinishPosition.onTEnter.AddListener(OnEnterExitArea);
        }

        public void DisableCustomer()
        {
            _customerState = State.INACTIVE;
            _rideStartPosition.onTEnter.RemoveListener(OnEnterStartArea);
            _rideFinishPosition.onTEnter.RemoveListener(OnEnterExitArea);
            Debug.Log("$$$ gained");
        }

        // ==============
        // states
        // ==============
        public void UpdateEntering()
        {
            DisablePhysics();
            
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance > 0.2f)
            {
                Vector3 direction = (_player.transform.position - transform.position).normalized;
                Vector3 newPosition = transform.position + direction * _moveSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(newPosition);
                _rigidbody.MoveRotation(_player.transform.rotation);
            }
            else
            {
                FixPosition();
                _customerState = State.RIDING;
            }
        }
        
        public void UpdateExiting()
        {
            UnfixPosition();
            
            float distance = Vector3.Distance(transform.position, _stopPosition.position);
            if (distance > 0.2f)
            {
                Vector3 direction = (_stopPosition.position - transform.position).normalized;
                Vector3 newPosition = transform.position + direction * _moveSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(newPosition);
                _rigidbody.MoveRotation(_stopPosition.rotation);
            }
            else
            {
                EnablePhysics();
                _customerState = State.FINISH;
            }
        }

        public void UpdateFinish()
        {
            gameObject.SetActive(false);
        }

        // ==============
        // trigger callbacks
        // ==============
        public void OnEnterStartArea(Collider other)
        {
            if (other.CompareTag(TagManager.GetTag(TagManager.ProjectTags.Player)))
            {
                _rideStartPosition.gameObject.SetActive(false);
                _rideFinishPosition.gameObject.SetActive(true);
                
                // TODO: lock player input and prevent two customers entering the taxi cab
                
                _player = other.gameObject;
                _customerState = State.ENTERING;
            }
        }

        public void OnEnterExitArea(Collider other)
        {
            if (other.CompareTag(TagManager.GetTag(TagManager.ProjectTags.Player)))
            {
                _rideFinishPosition.gameObject.SetActive(false);
                _player = null;
                _customerState = State.EXITING;
            }
        }

        // ==============
        // control
        // ==============
        public void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _characterCollider.isTrigger = true;
        }

        public void EnablePhysics()
        {
            _characterCollider.isTrigger = false;
            _rigidbody.isKinematic = false;
        }
        
        public void FixPosition()
        {
            // Follow
            _transformFollow = gameObject.AddComponent<TransformFollow>();
            _transformFollow.FollowTarget = _player.transform;
            
            // Visuals
            _characterMesh.enabled = false;
            _arrowHint.enabled = false;
        }

        public void UnfixPosition()
        {
            // Parenting
            Destroy(_transformFollow);
            _transformFollow = null;
            
            // Visuals
            _characterMesh.enabled = true;
        }
        #endregion
    }
}