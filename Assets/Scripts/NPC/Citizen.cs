using UnityEngine.AI;
using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;
using Delegates;
using SpeedTaxi.ScoreSystem;
using SpeedTaxi.VFX;
using UnityEngine.Events;

namespace SpeedTaxi.NPCSystem
{
    public enum State
    {
        IDLE,
        WALK,
        STARTLED,
        RUN,
        DEAD,
        RESPAWN
    }

    public class Citizen : MonoBehaviour
    {
        #region CONSTANTS
        // Score
        private const int KILL_SCORE_MULTIPLIER = 5;
        
        // VFX
        private const string VFX_BLOODSPLATTER = "bloodsplatter";
        private const string VFX_BLOODPUDDLE = "bloodpuddle";
        #endregion

        #region FIELDS
        [Header("AI Settings")]
        [SerializeField] private string _surfaceObjectName = "NPCNavMesh"; 
        
        // AI Navigation
        private NavMeshAgent _agent;
        private NavMeshSurface _surface;

        // Physics
        private Rigidbody _rigidbody;
        private float _carCollisionForce = 15f;
        
        // VFX
        private ParticleManager _particleManager;
        
        // Destination Settings
        private float _destinationMaxRange = 150f;
        private bool _generatingDestination = false;

        // Health
        private Health _health;
        private float _maxDeathTimer = 10f;
        private float _deadTimer;

        // Score
        private ScoreSupplier _scoreSupplier;
        #endregion

        #region STATE
        [SerializeField] private State _citizenState = State.RESPAWN;
        #endregion
        
       #region DELEGATES
       public GenericDelegates.BooleanDelegate _checkImmobileRigidbody;

       public bool CheckImmobileRigidbody()
       {
           return (_rigidbody.linearVelocity.magnitude < 0.02f && _rigidbody.angularVelocity.magnitude < 0.02f);
       }
       #endregion

        #region UNITY EVENTS
        public UnityEvent onCitizenDeath;
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            InitializeCitizen();
        }

        private void OnDisable()
        {
            _citizenState = State.RESPAWN;
        }

        private void Update()
        {
            switch (_citizenState)
            {
                case State.IDLE:
                    OnIdleState();
                    break;
                case State.WALK:
                    OnWalkState();
                    break;
                case State.STARTLED:
                    OnStartledState();
                    break;
                case State.RUN:
                    OnRunState();
                    break;
                case State.DEAD:
                    OnDeadState();
                    break;
                default:
                    Debug.Log($"Unnacounted state reached on {gameObject.name}");
                    break;
            }
        }
        #endregion

        #region CUSTOM METHODS
        private void OnIdleState()
        {
            if (!_generatingDestination)
                StartCoroutine(GetDestination());
            
            if (_agent.hasPath)
            {
                _citizenState = State.WALK;
            }
        }
        private void OnWalkState()
        {
            if (!_agent.hasPath && !_generatingDestination)
                StartCoroutine(GetDestination());
        }
        private void OnStartledState()
        {

        }
        private void OnRunState()
        {

        }
        private void OnDeadState()
        {
            _deadTimer -= Time.deltaTime;
            if (_deadTimer < 0)
                gameObject.SetActive(false);
        }
        private void OnRespawnState()
        {
            InitializeCitizen();
        }

        // Initialization
        private void InitializeCitizen()
        {
            // refs init
            if (_agent == null)
                _agent = GetComponent<NavMeshAgent>();
            if (_surface == null)
            {
                _surface = GenericUtilities.Instance.FindNavMeshSurface(_surfaceObjectName);
            }
            if (_health == null)
            {
                _health = GetComponent<Health>();
                _health.onDie.AddListener(Die);
            }
            if (_scoreSupplier == null)
            {
                _scoreSupplier = GetComponentInChildren<ScoreSupplier>();
                _scoreSupplier.InitializeScore(KILL_SCORE_MULTIPLIER);
            }

            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            
            if (_particleManager == null)
                _particleManager = GetComponentInChildren<ParticleManager>();
            
            // Delegates
            if (_checkImmobileRigidbody == null)
                _checkImmobileRigidbody = CheckImmobileRigidbody;

            // Reset 
            _health.Alive = true;
            _deadTimer = _maxDeathTimer;
            _agent.enabled = true;
            _rigidbody.isKinematic = true;
            if (_agent.hasPath)
                _agent.ResetPath();

            // state init
            _citizenState = State.IDLE;

        }
        
        // Behavior
        private void Die()
        {
            _citizenState = State.DEAD;
            onCitizenDeath?.Invoke();

            // vfx
            _particleManager.GetVFX(VFX_BLOODSPLATTER).Play();
            StartCoroutine(_particleManager.GetVFX(VFX_BLOODPUDDLE).PlayWhen(_checkImmobileRigidbody, 1.5f));
            
            // disable navmesh
            _agent.enabled = false;
            _rigidbody.isKinematic = false;

            transform.position = transform.position + new Vector3(0, 1, 0);
            _rigidbody.AddForce(_carCollisionForce * Vector3.up, ForceMode.Impulse);
            
        }


        // AI
        public IEnumerator GetDestination()
        {
            _generatingDestination = true;

            while (_generatingDestination)
            {
                Vector3 destination = GenericUtilities.Instance.GenerateDestinationOnSurface(transform, _destinationMaxRange, _surface);
                if (destination != Vector3.zero)
                {
                    _agent.SetDestination(destination);
                    _generatingDestination = false;
                }
                yield return null;
            }
        }
        #endregion
    }
}