using System;
using UnityEngine;

public abstract class IGameDataModel : MonoBehaviour
{
    //События для оповещения об изменениях
    public abstract event Action<EnumState> OnChangeState;
    public abstract event Action<float> OnChangeCurrentGameTime;
    public abstract event Action<int> OnChangedCountGameScore;
    public abstract event Action<int> OnChangedCountMisses;

    /*********************************************
     *  Методы для взаимодействия с данными модели
     *********************************************/

    public abstract void ChangeState(EnumState newState);
    public abstract void AddTime(float deltaSeconds);
    public abstract void ChangeTime(float time);
    public abstract void AddMisses(int countMisses);
    public abstract void AddScore(int addScore);
    public abstract void ChangeCountGameScore(int newCountGameScore);
    public abstract void ChangeCountMisses(int newCountMisses);
}
