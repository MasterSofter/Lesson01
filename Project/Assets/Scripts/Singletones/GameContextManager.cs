using UnityEngine;
using System.Collections.Generic;

public class GameContextManager : MonoBehaviour
{
    public static GameContextManager Instance = null; // Экземпляр объекта
    private int coins = 0;

    public GameObject[] fruits;
    public GameObject[] superFruits;
    public GameObject bomb;

    public void SubCoins(int value){
        coins -= value;
    }
    public void AddCoins(int value){
        coins += value;
    }
    public int GetCoins() => coins;


    void Awake()
    {
        if (Instance == null)
            Instance = this; // Задаем ссылку на экземпляр объекта
    }
}
