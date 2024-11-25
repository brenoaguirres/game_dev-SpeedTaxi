using UnityEngine;

public class PositionHint : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private float _spinSpeed = 30f;
    #endregion 
    void Update()
    {
        transform.Rotate(transform.up, _spinSpeed * Time.deltaTime);
    }
}
