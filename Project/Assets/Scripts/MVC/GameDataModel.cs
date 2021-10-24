using System;
public class GameDataModel : IGameDataModel
{
    public EnumState _state;           //текущее игровое состояния
    public float _currentGameTime;     //текущее игровое время
    public int _countGameScore;        //игровые очки
    public int _countMisses;

    public override event Action<EnumState> OnChangeState;
    public override event Action<float> OnChangeCurrentGameTime;
    public override event Action<int> OnChangedCountGameScore;
    public override event Action<int> OnChangedCountMisses;

    //События для оповещения об изменениях

   
    public override void AddTime(float deltaSeconds) {
        _currentGameTime += deltaSeconds;
        OnChangeCurrentGameTime?.Invoke(_currentGameTime);
    }

    public override void AddMisses(int countMisses){
        _countMisses += countMisses;
        OnChangedCountMisses?.Invoke(_countMisses);
    }


    public override void AddScore(int addScore){
        _countGameScore += addScore;
        OnChangedCountGameScore?.Invoke(_countGameScore);
    }

    public override void ChangeTime(float time){
        _currentGameTime = time;
        OnChangeCurrentGameTime?.Invoke(_currentGameTime);
    }

    public override void ChangeState(EnumState newState)
    {
        _state = newState;
        OnChangeState?.Invoke(newState);
    }

    public override void ChangeCountGameScore(int newCountGameScore) {
        _countGameScore = newCountGameScore;
        OnChangedCountGameScore?.Invoke(_countGameScore);
    }
    public override void ChangeCountMisses(int newCountMisses){
        _countMisses = newCountMisses;
        OnChangedCountMisses?.Invoke(_countMisses);
    }

}
