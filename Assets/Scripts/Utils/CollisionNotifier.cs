using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedTaxi.Utils
{
    public class CollisionNotifier : MonoBehaviour
    {
        #region EVENTS
        public UnityEvent<Collider> onTEnter;
        public UnityEvent<Collider> onTStay;
        public UnityEvent<Collider> onTExit;
        public UnityEvent<Collision> onCEnter;
        public UnityEvent<Collision> onCStay;
        public UnityEvent<Collision> onCExit;
        #endregion
        
        #region UNITY CALLBACKS
        // triggers
        public void OnTriggerEnter(Collider other)
        {
            onTEnter?.Invoke(other);
        }

        public void OnTriggerStay(Collider other)
        {
            onTStay?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            onTExit?.Invoke(other);
        }

        // collisions
        public void OnCollisionEnter(Collision other)
        {
            onCEnter?.Invoke(other);
        }

        public void OnCollisionStay(Collision other)
        {
            onCStay?.Invoke(other);
        }

        public void OnCollisionExit(Collision other)
        {
            onCExit?.Invoke(other);
        }
        #endregion
    }
}