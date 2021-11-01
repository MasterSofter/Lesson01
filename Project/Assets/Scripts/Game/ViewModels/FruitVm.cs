using UnityEngine;
using EventBus.Interfaces;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FruitVm : MonoBehaviour
{
    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    public IEventBus _eventBus;
    public FruitDm _fruitDm;
    public FruitDm FruitDm => _fruitDm;

    
    // Событие пересечения с фруктом ножа
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Blade")
            CutFruit(collision);
        if (collision.gameObject.tag == "DestroyerObjects")
            _eventBus.GetEvent<FruitMissedEvent<FruitVm>>().Publish(this);
    }

    // Функция, для резки фруктов 
    private void CutFruit(Collision2D collision)
    {
        GameObject newGameObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collision.gameObject.GetComponent<Blade>().DirectionBlade;
        newGameObj.transform.right = bladeDirection;
        newGameObj.GetComponent<CuttingFruitVm>().Init(_eventBus, _fruitDm);

        _eventBus.GetEvent<FruitCutEvent<FruitVm>>().Publish(this); //сообщили о разрезе фрукта
    }

    public void Init(IEventBus eventBus, FruitDm fruitDm) {
        _eventBus = eventBus;
        _fruitDm = fruitDm;
    }
}
