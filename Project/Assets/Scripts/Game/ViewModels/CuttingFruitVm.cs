using UnityEngine;
using EventBus.Interfaces;

public class CuttingFruitVm : MonoBehaviour
{
    [SerializeField] private float _startForce;
    private Rigidbody _rigidBody;
    public IEventBus _eventBus;
    public FruitDm _frutDm;

    public FruitDm FruitDm => _frutDm;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DestroyerObjects")
            if(_eventBus != null)
            _eventBus.GetEvent<DestroyCuttingFruitEvent<CuttingFruitVm>>().Publish(this);
    }

    public void Init(IEventBus eventBus, FruitDm fruitDm)
    {
        _eventBus = eventBus;
        _frutDm = fruitDm;

        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody != null) _rigidBody.AddForce(transform.up * _startForce, ForceMode.Impulse);
    }
}
