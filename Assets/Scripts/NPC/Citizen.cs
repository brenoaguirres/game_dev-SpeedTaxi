using UnityEngine.AI;
using UnityEngine;
using Unity.AI.Navigation;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.HID;
using SpeedTaxi.ScoreSystem;
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
        private const int KILL_SCORE_MULTIPLIER = 5;
        #endregion

        #region FIELDS
        [Header("AI Settings")]
        [SerializeField] private string _surfaceObjectName = "NPCNavMesh"; 
        
        // AI Navigation
        private NavMeshAgent _agent;
        private NavMeshSurface _surface;

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

            // Reset 
            _health.Alive = true;
            _deadTimer = _maxDeathTimer;
            _agent.enabled = true;
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

            // disable navmesh
            _agent.enabled = false;
        }

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