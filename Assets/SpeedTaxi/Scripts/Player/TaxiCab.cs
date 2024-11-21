using UnityEngine;

public class TaxiCab : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _steerSpeed = 50f;
    #endregion
    private void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * _steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        transform.Rotate(0, steerAmount, 0);
        transform.Translate(0, 0,moveAmount);
    }
}
