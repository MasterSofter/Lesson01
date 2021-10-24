using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject _explodedFruitPrefub;   //префаб фрукта, разбитого на части
    [SerializeField] private int _costClick;                    //стоимость одного разреза фрукта
    [SerializeField] private int _timeLifeSeconds = 12;         //время жизни

    public Action<int> OnDestroyFruit;

    void Start() => Destroy(gameObject, _timeLifeSeconds);

    
    // Событие пересечения с фруктом ножа
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Blade")
            CutFruit(collision); 
    }

    // Функция, для резки фруктов 
    protected void CutFruit(Collision2D collision)
    {
        GameObject newGameObj = Instantiate(_explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collision.gameObject.GetComponent<BladeMechanic>().DirectionBlade;
        newGameObj.transform.right = bladeDirection;

        OnDestroyFruit?.Invoke(_costClick);
        Destroy(gameObject);
    }


    
}
