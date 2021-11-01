using UnityEngine;
using EventBus.Interfaces;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BombVm : MonoBehaviour
{
    [SerializeField] private GameObject _modelBomb;
    [SerializeField] private GameObject _exlposionBombParticle;
    public IEventBus _eventBus;
    public BombDm _bombDm;
    public BombDm BombDm => _bombDm;


    // Событие пересечения с фруктом ножа
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blade")
            ExplodeBomb(collision);
        if (collision.gameObject.tag == "DestroyerObjects")
            _eventBus.GetEvent<DestroyGameObjectEvent<BombVm>>().Publish(this);
    }


    private void ExplodeBomb(Collision2D collision) {
        _modelBomb.active = false;
        _exlposionBombParticle.active = true;
        _eventBus.GetEvent<ExplodedBombEvent<BombVm>>().Publish(this);
    } 
 

    public void Init(IEventBus eventBus, BombDm bombDm)
    {
        _eventBus = eventBus;
        _bombDm = bombDm;
    }
}

