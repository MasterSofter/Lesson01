using UnityEngine;
using System.Collections;

public class CuttingFruit : MonoBehaviour
{
    [SerializeField] private float _startForce;
    [SerializeField] protected int _timeLifeSeconds = 12;
    private Rigidbody _rigidBody;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if(_rigidBody != null) _rigidBody.AddForce(transform.up * _startForce, ForceMode.Impulse);
        Destroy(gameObject, _timeLifeSeconds);
    }
}
