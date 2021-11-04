using System.Collections;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class ButtonPlayArcadeGame : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    public IEventBus _eventBus;

    [Inject]
    public void Construct(IEventBus eventBus) => _eventBus = eventBus;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Blade bladeComponent = collider.gameObject.GetComponent<Blade>();
        if (bladeComponent != null) StartCoroutine(CutFruit(collider));
    }


    protected IEnumerator CutFruit(Collider2D collider)
    {
        _model.active = false;

        GameObject newGameObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collider.gameObject.GetComponent<Blade>().DirectionBlade;
        newGameObj.transform.right = bladeDirection;

        yield return new WaitForSeconds(0.5f);
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Publish(EnumScene.ArcadeGameScene);
        Destroy(gameObject);

    }
}