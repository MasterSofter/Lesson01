using System.Collections;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class ButtonPlayClassicGame : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    public IEventBus _eventBus;

    [Inject]
    public void Construct(IEventBus eventBus) => _eventBus = eventBus;
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Blade bladeComponent = collision.gameObject.GetComponent<Blade>();
        if (bladeComponent != null) StartCoroutine(CutFruit(collision));
 
    }

    protected IEnumerator CutFruit(Collision2D collision)
    {
        _model.active = false;

        GameObject newGameObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collision.gameObject.GetComponent<Blade>().DirectionBlade;
        newGameObj.transform.right = bladeDirection;

        yield return new WaitForSeconds(0.5f);
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Publish(EnumScene.ClassicGameScene);
        Destroy(gameObject);

    }
}
