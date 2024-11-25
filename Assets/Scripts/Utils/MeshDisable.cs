using UnityEngine;
using System.Collections.Generic;

namespace SpeedTaxi.Utils
{
    public class MeshDisable : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshRenderers = new();

        private void Awake()
        {
            foreach (var _mr in _meshRenderers)
            {
                _mr.enabled = false;
            }
        }
    }
}
