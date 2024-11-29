using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Delegates;
using Unity.VisualScripting;

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

        public IEnumerator PlayDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            _particleVFX.Play();
        }

        public IEnumerator PlayWhen(GenericDelegates.BooleanDelegate function)
        {
            bool vfxStarted = false;

            while (!vfxStarted)
            {
                vfxStarted = function();
                if (vfxStarted)
                {
                    _particleVFX.Play();
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
        
        public IEnumerator PlayWhen(GenericDelegates.BooleanDelegate function, float delay)
        {
            yield return new WaitForSeconds(delay);
            bool vfxStarted = false;

            while (!vfxStarted)
            {
                vfxStarted = function();
                if (vfxStarted)
                {
                    _particleVFX.Play();
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        public void PlayOnSurface(Vector3 surfaceNormal)
        {
            
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
