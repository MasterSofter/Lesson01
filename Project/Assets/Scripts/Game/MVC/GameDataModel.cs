using EventBus.Interfaces;

public sealed class GameDataModel
{
    private EnumGameState _state;        //текущее игровое состояния
    private float _gameTime;         //текущее игровое время
    private int _gameScore;          //игровые очки
    private int _countMisses;

    private IEventBus _eventBus;

    public GameDataModel(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public float GameTime
    {
        get { return _gameTime; }
        set
        {
            _gameTime = value;
            _eventBus.GetEvent<GameDataModelChangedEvent<float>>().Publish(_gameTime);
        }
    }

    public int CountMisses
    {
        get { return _countMisses; }
        set
        {
            _countMisses = value;
            _eventBus.GetEvent<GameDataModelChangedEvent<int>>().Publish(_countMisses);
        }
    }


    public int GameScore
    {
        get { return _gameScore; }
        set
        {
            _gameScore = value;
            _eventBus.GetEvent<GameDataModelChangedEvent<int>>().Publish(_gameScore);
        }
    }
    public EnumGameState GameState
    {
        get { return _state; }
        set
        {
            _state = value;
            _eventBus.GetEvent<GameDataModelChangedEvent<EnumGameState>>().Publish(_state);
        }
    }
}
