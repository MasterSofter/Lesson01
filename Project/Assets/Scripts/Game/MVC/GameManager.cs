using System.Collections;
using UnityEngine;
using EventBus.Interfaces;

public class GameManager
{
    private IEventBus _eventBus;
    private readonly IGameViewer _gameViewer;
    private readonly GameDataModel _gameDataModel;
    private float _timeSpawnFruitCurrent = 0;
    private float _timeSpawSuperFruitCurrent = 0;
    private float _timeBombSpawnCurrent = 0;

    private float _timeSpawnFruit = 2f;
    private float _timeSpawSuperFruit = 10f;
    private float _timeBombSpawn = 6f;

   
    public void OnUdpateCurrentGameTime(float seconds) { }
    public void OnUpdateCountMisses(int newCountMisses) { }

    /******************************************************************
     *             Обработчики событий от наших игровых объектов
     ******************************************************************/

    public void OnFruitCut(FruitDm fruit)
    {
        if(_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameScore += fruit.Score;
    }

    public void OnFruitCut(SuperFruitDm fruit)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameScore += fruit.Score;
    }


    public void OnFruitMissed(FruitDm dm)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameState = EnumGameState.GameOver;
    }

    public void OnFruitMissed(SuperFruitDm superFruitDm) {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameState = EnumGameState.GameOver;
    }

    public void OnBombExploded(BombDm bomb)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameState = EnumGameState.GameOver;

    }


    public void OnGameStart(GameStartEventArg arg){
        if (arg.Started){
            _gameDataModel.GameTime = 0;
            _gameDataModel.GameScore = 0;
        }
        else{
            _gameDataModel.GameState = EnumGameState.Game;
        }
    }
    public void OnResumeGame(GameResumeEventArg arg){
        if (arg.Started)
        {

        }
        else {
            _gameDataModel.GameState = EnumGameState.Game;
        }
    }
    public void OnPauseGame(GamePauseEventArg arg) {
        if (arg.Started)
        {
            _gameDataModel.GameState = EnumGameState.Pause;
        }
        else {

        }
    }

  
    private void SpawnBomb() => _gameViewer.SpawnBomb(new BombDm());
    private void SpawnFruit() => _gameViewer.SpawnFruit(new WatermelonDm(1));
    private void SpawnSuperFruit() => _gameViewer.SpawnSuperFruit(new GarnetDm(5));


   

    public void OnTimeTick(TimeTickEventArg arg)
    {
        if (_gameDataModel.GameState == EnumGameState.Game) {
            _timeSpawnFruitCurrent += arg.DeltaTime;
            _timeSpawSuperFruitCurrent += arg.DeltaTime;
            _timeBombSpawnCurrent += arg.DeltaTime;
            _gameDataModel.GameTime += arg.DeltaTime;

            if (_timeSpawSuperFruitCurrent >= _timeSpawSuperFruit)
            {
                SpawnSuperFruit();
                _timeSpawSuperFruitCurrent = 0;
            }

            if (_timeSpawnFruitCurrent >= _timeSpawnFruit)
            {
                SpawnFruit();
                _timeSpawnFruitCurrent = 0;
            }

            if(_timeBombSpawnCurrent >= _timeBombSpawn)
            {
                SpawnBomb();
                _timeBombSpawnCurrent = 0;
            }
        }
        
            
    }

    private void InitBusEvents() {
        _eventBus.GetEvent<GameStartEvent>().Subscribe(OnGameStart);
        _eventBus.GetEvent<GameResumeEvent>().Subscribe(OnResumeGame);
        _eventBus.GetEvent<GamePauseEvent>().Subscribe(OnPauseGame);
        _eventBus.GetEvent<FruitCutEvent<FruitDm>>().Subscribe(OnFruitCut);
        _eventBus.GetEvent<FruitCutEvent<SuperFruitDm>>().Subscribe(OnFruitCut);
        _eventBus.GetEvent<FruitMissedEvent<FruitDm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<FruitMissedEvent<SuperFruitDm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<ExplodedBombEvent<BombDm>>().Subscribe(OnBombExploded);
        _eventBus.GetEvent<TimeTickEvent>().Subscribe(OnTimeTick);
    }

    public GameManager(IEventBus eventBus, IGameViewer gameViewer , GameDataModel gameDataModel )
    {
        _eventBus = eventBus;
        _gameDataModel = gameDataModel;
        _gameViewer = gameViewer;

        InitBusEvents();
        _gameDataModel.GameState = EnumGameState.StartGame;
    }
}
