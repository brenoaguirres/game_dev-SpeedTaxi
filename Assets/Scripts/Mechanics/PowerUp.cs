using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected Power Up");
        Destroy(gameObject);
    }

    void Update()
    {
        transform.RotateAround(transform.up, 10f * Time.deltaTime);
    }
}
