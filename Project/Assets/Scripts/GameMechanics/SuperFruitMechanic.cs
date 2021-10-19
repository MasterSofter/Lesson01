using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SuperFruitMechanic : FruitMechanic
{
    bool isClicked = false;
    Collider2D colliderBlade;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Blade")
        {
            if (!isClicked) {
                isClicked = true;
                StartCoroutine(Growth());
                colliderBlade = collider;
            }
            AddCoins();
        }   
    }

    private IEnumerator Growth() {
        while (transform.localScale.x < 1.4f)
        {
            CameraMain.Instance.Focus(gameObject);
            transform.localScale += new Vector3(0.004f, 0.004f, 0.004f);
            yield return new WaitForFixedUpdate();
        }
        CameraMain.Instance.Defocus();
        CutFruit(colliderBlade);
    }

    private void CutFruit(Collider2D collider)
    {
        GameObject newObj = MainGameManager.Instance.AddGameObject(explodedFruitPrefub, transform.position, Quaternion.identity);
        Vector2 bladeDirection = collider.gameObject.GetComponent<BladeMechanic>().DirectionBlade;
        newObj.transform.right = bladeDirection;
        MainGameManager.Instance.DestroyGameObject(gameObject);
    }
}
