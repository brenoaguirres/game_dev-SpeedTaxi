using System;
using SpeedTaxi.Utils;
using UnityEngine;
using UnityEngine.Animations;

namespace SpeedTaxi.CustomerSystem
{
    public enum State
    {
        INACTIVE,
        WAITING,
        ENTERING,
        RIDING,
        PAYMENT,
        FINISH
    }

    public class Customer : MonoBehaviour
    {
        #region FIELDS
        [Header("Customer Behavior")] 
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _turnSpeed = 10f;

        [Space(2)] 
        [Header("Control")] 
        [SerializeField] private CollisionNotifier _collectArea;
        
        private Rigidbody _rigidbody;
        private GameObject _player;
        private ParentConstraint _parentConstraint;
        
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
                default:
                    Debug.Log("Unaccounted state reached.");
                    break;
            }
        }
        #endregion

        #region CUSTOM METHODS
        public void InitializeCustomer()
        {
            _customerState = State.WAITING;

            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            
            _collectArea.onTEnter.AddListener(OnEnterCollectArea);
        }

        public void DisableCustomer()
        {
            _customerState = State.INACTIVE;
            _collectArea.onTEnter.RemoveListener(OnEnterCollectArea);
        }

        public void UpdateEntering()
        {
            _rigidbody.isKinematic = true;
            _characterCollider.isTrigger = true;
            
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

        public void OnEnterCollectArea(Collider other)
        {
            if (other.CompareTag(TagManager.GetTag(TagManager.ProjectTags.Player)))
            {
                _collectArea.gameObject.SetActive(false);
                _player = other.gameObject;
                _customerState = State.ENTERING;
            }
        }

        public void FixPosition()
        {
            // Parenting
            _parentConstraint = gameObject.AddComponent<ParentConstraint>();
            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = _player.transform;
            _parentConstraint.SetSource(0, source);
            _parentConstraint.constraintActive = true;
            
            // Visuals
            _characterMesh.enabled = false;
        }
        #endregion
    }
}