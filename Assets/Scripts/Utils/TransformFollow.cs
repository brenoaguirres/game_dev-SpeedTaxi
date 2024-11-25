using UnityEngine;

namespace SpeedTaxi.Utils
{
    public class TransformFollow : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;

        public Transform FollowTarget
        {
            get => _followTarget;
            set => _followTarget = value;
        }
        public void Update()
        {
            transform.position = _followTarget.position;
            transform.rotation = _followTarget.rotation;
        }
    }
}