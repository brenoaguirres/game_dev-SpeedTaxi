using UnityEngine;
using System.Collections.Generic;

namespace SpeedTaxi.Utils
{
    public class OnlyOnEditor : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _components = new();

        private void Awake()
        {
            foreach (var component in _components)
            {
                component.enabled = false;
            }
        }
    }
}
