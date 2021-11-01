using UnityEngine;
using EventBus.Interfaces;

public class CuttingSuperFruitVm: MonoBehaviour
{
    [SerializeField] private float _startForce;
    private Rigidbody _rigidBody;
    public IEventBus _eventBus;
    public SuperFruitDm _superFrutDm;

    public SuperFruitDm SuperFruitDm => _superFrutDm;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DestroyerObjects")
            if(_eventBus != null)
            _eventBus.GetEvent<DestroyCuttingFruitEvent<CuttingSuperFruitVm>>().Publish(this);
    }


    public void Init(IEventBus eventBus, SuperFruitDm fruitDm)
    {
        _eventBus = eventBus;
        _superFrutDm = fruitDm;

        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody != null) _rigidBody.AddForce(transform.up * _startForce, ForceMode.Impulse);
    }
}
