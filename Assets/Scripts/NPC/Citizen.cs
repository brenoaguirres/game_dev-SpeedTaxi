using UnityEngine.AI;
using UnityEngine;
using Unity.AI.Navigation;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.HID;

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
        #region FIELDS
        [Header("AI Settings")]
        [SerializeField] private string _surfaceObjectName = "NPCNavMesh"; 
        
        // AI Navigation
        private NavMeshAgent _agent;
        private NavMeshSurface _surface;

        // Destination Settings
        private float _destinationMaxRange = 150f;
        private bool _generatingDestination = false;
        #endregion

        #region STATE
        [SerializeField] private State _citizenState = State.RESPAWN;
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
                _agent = GetComponentInParent<NavMeshAgent>();
            if (_surface == null)
            {
                _surface = GenericUtilities.Instance.FindNavMeshSurface(_surfaceObjectName);
            }

            // state init
            _citizenState = State.IDLE;
        }
        
        // AI
        private NavMeshSurface FindNavMeshSurface()
        {
            List<NavMeshSurface> surfaceCollection = FindObjectsOfType<NavMeshSurface>().ToList();
            NavMeshSurface returnSurface = null;

            foreach (NavMeshSurface surface in surfaceCollection)
            {
                if (surface.gameObject.name == _surfaceObjectName)
                    returnSurface = surface; 
            }

            if (returnSurface != null)
            {
                return returnSurface;
            }
            else
            {
                Debug.LogError($"No navmesh surface found on {gameObject.name}");
                return null;
            }
        }

        private bool GenerateAIDestination()
        {
            Vector3 randomPoint = transform.position + new Vector3(
                Random.Range(-_destinationMaxRange, _destinationMaxRange), _surface.transform.position.y, Random.Range(-_destinationMaxRange, _destinationMaxRange)
                );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, _destinationMaxRange, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                Debug.Log(hit);
                return true;
            }
            else
            {
                return false;
            }
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