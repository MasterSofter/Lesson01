using UnityEngine;

public class GizmosObject : MonoBehaviour
{
    [SerializeField]
    private Color gizmoColor;
    [SerializeField]
    private Vector3 sizeCube;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; //Назначаем цвет нашему объекту

        //Рисуем Gizmos Куб.Он принимает два параметра. 1.Позиция Объекта 2.Размер Объекта
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z), sizeCube);
    }
}
