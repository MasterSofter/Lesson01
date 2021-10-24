using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _modelBomb;
    [SerializeField] private GameObject _exlposionBombParticle;
    [SerializeField] private float _timeLifeSeconds;
    public event Action OnDestroyBomb;

    // Событие пересечения с фруктом ножа

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blade")
            DestroyBomb();
    }

    private void DestroyBomb(){
        _modelBomb.active = false;
        _exlposionBombParticle.active = true;

        Destroy(gameObject, 1.2f);
        OnDestroyBomb?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _timeLifeSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
