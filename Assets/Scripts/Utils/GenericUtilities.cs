using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GenericUtilities : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static GenericUtilities _instance;

    public static GenericUtilities Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GenericUtilities>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GenericUtilities).ToString());
                    _instance = singleton.AddComponent<GenericUtilities>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region UNITY CALLBACKS
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region CUSTOM METHODS
    public NavMeshSurface FindNavMeshSurface(string surfaceObjectName)
    {
        List<NavMeshSurface> surfaceCollection = FindObjectsOfType<NavMeshSurface>().ToList();
        NavMeshSurface returnSurface = null;

        foreach (NavMeshSurface surface in surfaceCollection)
        {
            if (surface.gameObject.name == surfaceObjectName)
                returnSurface = surface;
        }

        if (returnSurface != null)
        {
            return returnSurface;
        }
        else
        {
            return null;
        }
    }

    public Vector3 GenerateDestinationOnSurface(Transform t, float range, NavMeshSurface surface)
    {
        Vector3 randomPoint = t.position + new Vector3(
            Random.Range(-range, range), surface.transform.position.y, Random.Range(-range, range)
            );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    #endregion
}
