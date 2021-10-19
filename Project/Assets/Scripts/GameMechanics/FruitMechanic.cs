using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FruitMechanic : MonoBehaviour
{
    [SerializeField]
    protected GameObject explodedFruitPrefub;   //префаб фрукта, разбитого на части
    [SerializeField]
    protected int costClick;                    //стоимость одного разреза фрукта
    [SerializeField]
    protected int timeLifeSeconds = 12;         //время жизни фрукта

    protected Rigidbody2D rigidBody2D;
    protected CapsuleCollider2D capsuleCollider2D;
   
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        StartCoroutine(Life());
    }

    //Функция, реализующая отсчет времени жизни фрукта
    protected IEnumerator Life()
    {
        yield return new WaitForSeconds(timeLifeSeconds);
        MainGameManager.Instance.DestroyGameObject(gameObject);
    }
    // Событие пересечения с фруктом ножа
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blade")
        {
            CutFruit(collision); //разрезаем фрукт
            AddCoins();          //добавляем в общую казну игры монеты
        }
    }

    // Функция, для резки фруктов 
    protected void CutFruit(Collision2D collision) {
        GameObject newObj = MainGameManager.Instance.AddGameObject(explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collision.gameObject.GetComponent<BladeMechanic>().DirectionBlade;
        newObj.transform.right = bladeDirection;
        MainGameManager.Instance.DestroyGameObject(gameObject);
    }

    // Функкция, для добавления очков в казну игры
    protected void AddCoins(){
        GameContextManager.Instance.AddCoins(costClick);
    }
}
