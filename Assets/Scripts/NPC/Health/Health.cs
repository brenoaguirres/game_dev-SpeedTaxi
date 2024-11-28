using SpeedTaxi.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedTaxi.NPCSystem
{
    public class Health : MonoBehaviour
    {
        #region FIELDS
        private bool _alive = false;
        #endregion

        #region PROPERTIES
        public bool Alive 
        { 
            get { return _alive; } 
            set 
            { 
                _alive = value;
                if (value == false)
                {
                    onDie?.Invoke();
                }
            } 
        }
        #endregion

        #region EVENTS
        public UnityEvent onDie;
        #endregion

        #region UNITY CALLBACKS
        private void OnCollisionEnter(Collision collision)
        {
            if (!Alive) return;

            if (collision.gameObject.CompareTag(TagManager.ProjectTags.Player.ToString()))
            {
                float dot = Vector3.Dot(collision.GetContact(0).point, collision.gameObject.transform.forward);
                if (dot > 0.75f || dot < -0.75f)
                {
                    Alive = false;
                }
            }
        }
        #endregion
    }
}