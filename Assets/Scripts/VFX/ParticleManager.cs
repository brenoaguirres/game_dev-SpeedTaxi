using System;
using UnityEngine;
using System.Collections.Generic;

namespace SpeedTaxi.VFX
{
    [Serializable]
    public class VFX
    {
        #region FIELDS
        [SerializeField] private string _idVFX;
        [SerializeField] private ParticleSystem _particleVFX;
        #endregion
        
        #region PROPERTIES
        public string IdVFX { get { return _idVFX; } }
        #endregion
        
        #region CONSTRUCTORS
        public VFX(string id, ParticleSystem ps)
        {
            _idVFX = id;
            _particleVFX = ps;
        }
        #endregion
        
        #region CUSTOM METHODS
        public void Play()
        {
            _particleVFX.Play();
        }
        #endregion
    }
    
    public class ParticleManager : MonoBehaviour
    {
        #region FIELDS
        [Header("VFX Collection")]
        [SerializeField] private List<string> _ids = new();
        [SerializeField] private List<ParticleSystem> _particles = new();
        private List<VFX> _vfxList = new();
        #endregion
        
        #region UNITY CALLBACKS
        private void Start()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _vfxList.Add(new VFX(_ids[i], _particles[i]));
            }
        }
        #endregion
        
        #region CUSTOM METHODS
        public VFX GetVFX(string id)
        {
            VFX vfx = null;
            
            foreach (VFX fx in _vfxList)
            {
                if (fx.IdVFX == id)
                    vfx = fx;
            }

            if (vfx == null) Debug.LogError($"VFX not found by id: {id} ");
            return vfx;
        }
        #endregion
    }
}
