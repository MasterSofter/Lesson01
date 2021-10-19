using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Экземпляр объекта
    [SerializeField]
    private TMPro.TextMeshPro coinsText;
    [SerializeField]
    private GameObject gameOverText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        coinsText.text = GameContextManager.Instance.GetCoins().ToString();
    }

    public void ShowGameOver() {
        gameOverText.active = true;
    }
}
