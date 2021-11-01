using UnityEngine;

public class RotateScript : MonoBehaviour
{
    [SerializeField] Vector3 rotateVector;
    void Update() => transform.Rotate(rotateVector * Time.deltaTime);
}
