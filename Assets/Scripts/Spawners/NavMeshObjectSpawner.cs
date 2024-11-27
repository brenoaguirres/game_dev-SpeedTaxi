using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.AI.Navigation;

namespace SpeedTaxi.Spawners
{
    public class NavMeshObjectSpawner : MonoBehaviour
    {
        #region CONSTANTS
        private const int BATCH_SIZE = 10;
        private const float INIT_SPAWN_RANGE = 200;
        #endregion

        #region FIELDS
        [Header("Spawn Settings")]
        [SerializeField] private GameObject _spawnablePrefab;
        [SerializeField] private int _quantity = 50;
        [SerializeField] private Transform _spawnableParent;
        [SerializeField] private NavMeshSurface _spawnableSurface;
        [SerializeField] private Transform _startingSpawnPoint;
        private List<GameObject> _objects = new();
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            StartCoroutine(InstantiateXByFrame(_quantity, BATCH_SIZE));
        }
        #endregion

        #region CUSTOM METHODS
        private IEnumerator InstantiateXByFrame(int count, int batchSize)
        {
            int currentCount = 0;

            while (currentCount < count)
            {
                for (int i = 0; i < batchSize; i++)
                {
                    var obj = Instantiate(
                        _spawnablePrefab, 
                        GenericUtilities.Instance.GenerateDestinationOnSurface(_startingSpawnPoint, INIT_SPAWN_RANGE, _spawnableSurface), 
                        Quaternion.identity, 
                        _spawnableParent
                        );

                    obj.SetActive(true);
                    _objects.Add(obj);
                }

                currentCount = _objects.Count;
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion
    }
}
