using UnityEngine;
using System.Collections;
using EventBus.Interfaces;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SuperFruitVm : MonoBehaviour
{

    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    public IEventBus _eventBus;
    public SuperFruitDm _superFruitDm;

    public SuperFruitDm SuperFruitDm => _superFruitDm;

    private Collider2D _colliderBlade;
    private bool _isClicked = false;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Blade")
        {
            if (!_isClicked)
            {
                _isClicked = true;
                _colliderBlade = collider;
                StartCoroutine(Growth());
            }
            _eventBus.GetEvent<FruitCutEvent<SuperFruitVm>>().Publish(this);

        }
        if (collider.gameObject.tag == "DestroyerObjects")
            _eventBus.GetEvent<FruitMissedEvent<SuperFruitVm>>().Publish(this);

    }

    private IEnumerator Growth()
    {
        while (transform.localScale.x < 1.4f)
        {
            transform.localScale += new Vector3(0.004f, 0.004f, 0.004f);
            yield return new WaitForFixedUpdate();
        }
        DestroySuperFruit(_colliderBlade);
    }

    // Функция, для резки фруктов 
    protected void DestroySuperFruit(Collider2D collider)
    {
        GameObject newGameObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collider.gameObject.GetComponent<Blade>().DirectionBlade;
        newGameObj.transform.right = bladeDirection;
        newGameObj.GetComponent<CuttingSuperFruitVm>().Init(_eventBus, _superFruitDm);

        _eventBus.GetEvent<DestroyCuttingFruitEvent<SuperFruitVm>>().Publish(this);
    }

    public void Init(IEventBus eventBus, SuperFruitDm superFruitDm)
    {
        _eventBus = eventBus;
        _superFruitDm = superFruitDm;
    }
}
