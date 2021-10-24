using UnityEngine;

public class DestroyManager3D : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) => Destroy(collision.gameObject);
}
