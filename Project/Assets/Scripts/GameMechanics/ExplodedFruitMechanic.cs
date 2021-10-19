using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedFruitMechanic : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _startForce;
    [SerializeField]
    protected int timeLifeSeconds = 12;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if(_rigidBody != null)
            _rigidBody.AddForce(transform.up * _startForce, ForceMode.Impulse);
        StartCoroutine(Life());
    }

    protected IEnumerator Life()
    {
        yield return new WaitForSeconds(timeLifeSeconds);
        MainGameManager.Instance.DestroyGameObject(gameObject);
    }
}
