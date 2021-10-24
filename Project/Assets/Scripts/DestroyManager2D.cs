using System;
using UnityEngine;

public class DestroyManager2D : MonoBehaviour
{
    public Action OnDestroyMissedFruit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();

        if (fruit != null)
        {
            Destroy(collision.gameObject);
            OnDestroyMissedFruit?.Invoke();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
