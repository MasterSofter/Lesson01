using System.Collections;
using System.Collections.Generic;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class ArcadeGameManager : MonoBehaviour, IGameManager
{
    private IEventBus _eventBus;
    private IGameViewer _gameViewer;
    private IGameDataModel _gameDataModel;

    private float _timeSpawnFruitCurrent = 0;
    private float _timeSpawSuperFruitCurrent = 0;
    private float _timeBombSpawnCurrent = 0;
    private float _timeSpawnFruit = 1f;
    private float _timeSpawSuperFruit = 8f;
    private float _timeBombSpawn = 10f;


    private void SpawnBomb() => _gameViewer.SpawnBomb(new ArcadeBombDm(5));
    private void SpawnFruit() => _gameViewer.SpawnFruit(new WatermelonDm(1));
    private void SpawnSuperFruit() => _gameViewer.SpawnSuperFruit(new GarnetDm(5));


    /******************************************************************
     *             Обработчики событий от наших игровых объектов
     ******************************************************************/

    public void OnFruitCut(FruitDm fruit)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameScore += fruit.Score;
    }
    public void OnFruitCut(SuperFruitDm fruit)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameScore += fruit.Score;
    }
    public void OnFruitMissed(FruitDm dm)
    {
        if (_gameDataModel.GameState == EnumGameState.Game) {
            _gameDataModel.CountMisses++;
            _gameDataModel.GameScore -= dm.Score * _gameDataModel.CountMisses;
        }
            
    }
    public void OnFruitMissed(SuperFruitDm superFruitDm)
    {
        if (_gameDataModel.GameState == EnumGameState.Game){
            _gameDataModel.CountMisses++;
            _gameDataModel.GameScore -= superFruitDm.Score * _gameDataModel.CountMisses;
        }
    }
    public void OnBombExploded(BombDm bomb)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
            _gameDataModel.GameScore -= bomb.Score;

    }

    /******************************************************************
    *                   Реализация интерфейса
    ******************************************************************/


    public void OnGameStart(GameStartEventArg arg)
    {
        if (arg.Started)
        {
            _gameDataModel.GameTime = 60;
            _gameDataModel.GameScore = 0;
        }
        else
        {
            _gameDataModel.GameState = EnumGameState.Game;
        }
    }

    public void OnGameRestart(GameRestartEventArg arg)
    {
        if (!arg.Started) _gameDataModel.GameState = EnumGameState.StartGame;

    }

    public void OnResumeGame(GameResumeEventArg arg)
    {
        if (arg.Started)
        {

        }
        else
        {
            _gameDataModel.GameState = EnumGameState.Game;
        }
    }
    public void OnPauseGame(GamePauseEventArg arg)
    {
        if (arg.Started)
        {
            _gameDataModel.GameState = EnumGameState.Pause;
        }
        else
        {

        }
    }

    public void OnTimeTick(TimeTickEventArg arg)
    {
        if (_gameDataModel.GameState == EnumGameState.Game)
        {
            if (_gameDataModel.GameTime <= 0)
                _gameDataModel.GameState = EnumGameState.GameOver;
            else {
                _timeSpawnFruitCurrent += arg.DeltaTime;
                _timeSpawSuperFruitCurrent += arg.DeltaTime;
                _timeBombSpawnCurrent += arg.DeltaTime;
                _gameDataModel.GameTime -= arg.DeltaTime;

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

                if (_timeBombSpawnCurrent >= _timeBombSpawn)
                {
                    SpawnBomb();
                    _timeBombSpawnCurrent = 0;
                }
            }
        }
    }

    private void InitBusEvents()
    {
        _eventBus.GetEvent<GameStartEvent>().Subscribe(OnGameStart);
        _eventBus.GetEvent<GameRestartEvent>().Subscribe(OnGameRestart);
        _eventBus.GetEvent<GameResumeEvent>().Subscribe(OnResumeGame);
        _eventBus.GetEvent<GamePauseEvent>().Subscribe(OnPauseGame);
        _eventBus.GetEvent<FruitCutEvent<FruitDm>>().Subscribe(OnFruitCut);
        _eventBus.GetEvent<FruitCutEvent<SuperFruitDm>>().Subscribe(OnFruitCut);
        _eventBus.GetEvent<FruitMissedEvent<FruitDm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<FruitMissedEvent<SuperFruitDm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<ExplodedBombEvent<BombDm>>().Subscribe(OnBombExploded);
        _eventBus.GetEvent<TimeTickEvent>().Subscribe(OnTimeTick);
    }

    /******************************************************************
    *                       Инициализация
    ******************************************************************/

    [Inject]
    public void Construct(IEventBus eventBus, IGameViewer gameViewer, IGameDataModel gameDataModel)
    {
        _eventBus = eventBus;
        _gameDataModel = gameDataModel;
        _gameViewer = gameViewer;

        InitBusEvents();
        _gameDataModel.GameState = EnumGameState.StartGame;
        _gameDataModel.GameTime = 60;
    }
}
