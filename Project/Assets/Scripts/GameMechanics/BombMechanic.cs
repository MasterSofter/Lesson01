using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BombMechanic : MonoBehaviour
{
    [SerializeField]
    private GameObject particleExlosion;
    [SerializeField]
    private GameObject bombModel;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Blade")
        {
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion() {
        particleExlosion.active = true;
        bombModel.active = false;
        yield return new WaitForSeconds(0.2f);
        MainGameManager.Instance.DestroyGameObject(gameObject);
        MainGameManager.Instance.GameOver();
    }
}
