using UnityEngine;
using System.Collections;

public class CameraMain : MonoBehaviour
{
    public static CameraMain Instance = null; // Экземпляр объекта
    private bool isFocused = false;

    public bool IsFocused => isFocused;


    void Awake()
    {
        if (Instance == null)
            Instance = this; // Задаем ссылку на экземпляр объекта

        Camera.main.orthographicSize = 6.6f;
    }

    public void Focus(GameObject focusObject)
    {
        isFocused = true;
        transform.position = new Vector3 (focusObject.transform.position.x, focusObject.transform.position.y, 0);
        Camera.main.orthographicSize = 2.9f;
        MainGameManager.Instance.SlowDownSpeedGamePhysic();
    }

    public void Defocus()
    {
        isFocused = false;
        transform.position = Vector3.zero;
        Camera.main.orthographicSize = 6.6f;
        MainGameManager.Instance.ReturnDefaultSpeedGamePhysic();
    }
}
