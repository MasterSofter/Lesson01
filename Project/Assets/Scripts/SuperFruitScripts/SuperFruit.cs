using System.Collections;
using System;
using UnityEngine;

public class SuperFruit : MonoBehaviour
{
    private bool _isClicked = false;
    private Collider2D colliderBlade;
    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    [SerializeField] private int _costClick;                    //стоимость одного разреза фрукта
    [SerializeField] private int _timeLifeSeconds = 12;         //время жизни фрукта


    public event Action<int,Vector3> OnClickSuperFruit;
    public event Action OnDestroySuperFruit;


    void Start() => Destroy(gameObject, _timeLifeSeconds);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Blade")
        {
            if (!_isClicked)
            {
                _isClicked = true;
                colliderBlade = collider;
                StartCoroutine(Growth());
            }
            OnClickSuperFruit?.Invoke(_costClick, transform.position);
        }
    }

    private IEnumerator Growth()
    {
        while (transform.localScale.x < 1.4f)
        {
            transform.localScale += new Vector3(0.004f, 0.004f, 0.004f);
            yield return new WaitForFixedUpdate();
        }
        CutSuperFruit(colliderBlade);
    }

    private void CutSuperFruit(Collider2D collider) {
        GameObject newObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Destroy(gameObject);
        OnDestroySuperFruit?.Invoke();
    }
}
