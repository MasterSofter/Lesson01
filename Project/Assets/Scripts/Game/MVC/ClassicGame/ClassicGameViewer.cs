using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBus.Interfaces;
using EventBus.Composite.Presentation.Events;
using Zenject;

public class ClassicGameViewer : MonoBehaviour, IGameViewer
{
    private IEventBus _eventBus;
    public IGameDataModel _gameDataModel;

    private readonly Dictionary<FruitEnumeration, GameObject> _fruitsDictionary = new Dictionary<FruitEnumeration, GameObject>();

    [SerializeField] private GameObject _prefubWatermelon;
    [SerializeField] private GameObject _prefubGarnet;
    [SerializeField] private GameObject _bombPrefub;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _startSpawnFruitForce;
    [SerializeField] private float _startFruitTorgue;
    
    [SerializeField] private TMPro.TMP_Text _textTime;
    [SerializeField] private TMPro.TMP_Text _textGameScore;

    [SerializeField] private Animator _pauseButtonAnimator;
    [SerializeField] private Animator _pauseMenuAnimator;
    [SerializeField] private Animator _gameOverMenuAnimation;


    /******************************************************************
    *           Обработчики событий от наших игровых объектов
    ******************************************************************/

    public void OnFruitMissed(FruitVm fruitVm)
    {
        Destroy(fruitVm.gameObject);
        _eventBus.GetEvent<FruitMissedEvent<FruitDm>>().Publish(fruitVm.FruitDm);
    }

    public void OnFruitMissed(SuperFruitVm superFruitVm) {
        Destroy(superFruitVm.gameObject);
        _eventBus.GetEvent<FruitMissedEvent<SuperFruitDm>>().Publish(superFruitVm.SuperFruitDm);
    }

    public void OnFruitCut(FruitVm fruitVm) {
        Destroy(fruitVm.gameObject);
        _eventBus.GetEvent<FruitCutEvent<FruitDm>>().Publish(fruitVm.FruitDm);
    }

    public void OnFruitCut(SuperFruitVm fruitVm)
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        _eventBus.GetEvent<FruitCutEvent<SuperFruitDm>>().Publish(fruitVm.SuperFruitDm);
    }

    public void OnDestroyCuttingFruit(SuperFruitVm superFruitVm)
    {
        Destroy(superFruitVm.gameObject);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void OnDestroyCuttingFruit(CuttingFruitVm cuttingFruitVm) {
        Destroy(cuttingFruitVm.gameObject);
    }

    public void OnDestroyCuttingFruit(CuttingSuperFruitVm cuttingSuperFruitVm)
    {
        Destroy(cuttingSuperFruitVm.gameObject);
    }


    public void OnDestroyBomb(BombVm bombVm) => Destroy(bombVm.gameObject);

    public void OnBombExploded(BombVm bombVm) => _eventBus.GetEvent<ExplodedBombEvent<BombDm>>().Publish(bombVm.BombDm); 

    /******************************************************************
     *           Обработчики событий изменения модели данных игры
     ******************************************************************/

    public void OnChangedState(EnumGameState newState){
        switch(newState){
            case EnumGameState.StartGame:
                StartCoroutine(ShowStartGame());
                break;
            case EnumGameState.GameOver:
                StartCoroutine(ShowGameOver());
                break;
        }
    }
    public void OnChangedGameTime(float seconds){
        if (seconds == 0) { _textTime.text = "00:00"; return; }

        int minutes = (int)seconds / 60;
        int _seconds = (int)seconds - minutes * 60;

        string secondsStr = _seconds >= 10 ? _seconds.ToString() : $"0{_seconds}";
        string minitesStr = minutes >= 10 ? minutes.ToString() : $"0{minutes}";

        _textTime.text = $"{minitesStr}:{secondsStr}";
    }
    public void OnChangedGameScore(int newCountGameScore) => _textGameScore.text = newCountGameScore.ToString();
    public void OnChangedCountMisses(int newCountMisses) { }


    /******************************************************************
     *              Обработчики событий нажатия на кнопку UI
     ******************************************************************/
    
    public void OnGamePauseButtonClick() => StartCoroutine(ShowPauseGame());
    public void OnGameResumeButtonClick() => StartCoroutine(ShowResumeGame());
    public void OnGameRestartButtonClick() => StartCoroutine(ShowRestartGame());
    public void OnExitGame() {
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Publish(EnumScene.MainMenuScene);
    }



    /******************************************************************
     *                      Реализация интерфейса
     ******************************************************************/
    public void SpawnFruit(FruitDm fruitDm) {
        int rundomIndexSpawnPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);
        
        GameObject newGameObj = Instantiate(_fruitsDictionary[fruitDm.Id], _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startSpawnFruitForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startFruitTorgue);
        newGameObj.GetComponent<FruitVm>().Init(_eventBus, fruitDm);
    }

    public void SpawnSuperFruit(SuperFruitDm superFruitDm) {
        int rundomIndexSpawnPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);

        GameObject newGameObj = Instantiate(_fruitsDictionary[superFruitDm.Id], _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startSpawnFruitForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startFruitTorgue);
        newGameObj.GetComponent<SuperFruitVm>().Init(_eventBus, superFruitDm);
    }

    public void SpawnBomb(BombDm bombDm) {
        int rundomIndexSpawnPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);

        GameObject newGameObj = Instantiate(_bombPrefub, _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startSpawnFruitForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startFruitTorgue);

        newGameObj.GetComponent<BombVm>().Init(_eventBus, bombDm);
        

    }

    /******************************************************************
     *                             Корутины
     ******************************************************************/


    public IEnumerator ShowStartGame()
    {
        _eventBus.GetEvent<GameStartEvent>().Publish(new GameStartEventArg(true));

        _pauseButtonAnimator.Play("Show_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_ShowedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _eventBus.GetEvent<GameStartEvent>().Publish(new GameStartEventArg(false));
    }
    public IEnumerator ShowGameOver() {
        _eventBus.GetEvent<GameOverEvent>().Publish(new GameOverEventArg(true));

        _pauseButtonAnimator.Play("Hide_ButtonAnimation");

        _gameOverMenuAnimation.Play("Show_MenuAnimation");
        while (!_gameOverMenuAnimation.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _eventBus.GetEvent<GameOverEvent>().Publish(new GameOverEventArg(false));
    }
    public IEnumerator ShowRestartGame()
    {
        _eventBus.GetEvent<GameStartEvent>().Publish(new GameStartEventArg(true));

        if (_gameDataModel.GameState == EnumGameState.GameOver)
        {
            _gameOverMenuAnimation.Play("Hide_MenuAnimation");
            while (!_gameOverMenuAnimation.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
                yield return new WaitForEndOfFrame();
        }
        else
        {
            _pauseMenuAnimator.Play("Hide_MenuAnimation");
            while (!_pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
                yield return new WaitForEndOfFrame();
        }
        _pauseButtonAnimator.Play("Show_ButtonAnimation");

        _eventBus.GetEvent<GameStartEvent>().Publish(new GameStartEventArg(false));
    }
    public IEnumerator ShowResumeGame()
    {
        _eventBus.GetEvent<GameResumeEvent>().Publish(new GameResumeEventArg(true));

        _pauseMenuAnimator.Play("Hide_MenuAnimation");
        while (!_pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _pauseButtonAnimator.Play("Show_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_ShowedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _eventBus.GetEvent<GameResumeEvent>().Publish(new GameResumeEventArg(false));

    }
    public IEnumerator ShowPauseGame()
    {
        _eventBus.GetEvent<GamePauseEvent>().Publish(new GamePauseEventArg(true));

        _pauseButtonAnimator.Play("Hide_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_HidedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _pauseMenuAnimator.Play("Show_MenuAnimation");
        while (!_pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _eventBus.GetEvent<GamePauseEvent>().Publish(new GamePauseEventArg(false));
    }


    /******************************************************************
     *                             Инициализция
     ******************************************************************/

    private void InitGameObjects()
    {
        _fruitsDictionary.Add(FruitEnumeration.Watermelon, _prefubWatermelon);
        _fruitsDictionary.Add(FruitEnumeration.Garnet, _prefubGarnet);
    }

    private void InitBusEvents()
    {
        _eventBus.GetEvent<GameDataModelChangedEvent<EnumGameState>>().Subscribe(OnChangedState, ThreadOption.PublisherThread, true);
        _eventBus.GetEvent<GameDataModelChangedEvent<float>>().Subscribe(OnChangedGameTime, ThreadOption.PublisherThread, true);
        _eventBus.GetEvent<GameDataModelChangedEvent<int>>().Subscribe(OnChangedGameScore, ThreadOption.PublisherThread, true);
        _eventBus.GetEvent<GameDataModelChangedEvent<int>>().Subscribe(OnChangedCountMisses, ThreadOption.PublisherThread, true);
        _eventBus.GetEvent<FruitMissedEvent<FruitVm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<FruitMissedEvent<SuperFruitVm>>().Subscribe(OnFruitMissed);
        _eventBus.GetEvent<DestroyCuttingFruitEvent<CuttingFruitVm>>().Subscribe(OnDestroyCuttingFruit);
        _eventBus.GetEvent<DestroyCuttingFruitEvent<CuttingSuperFruitVm>>().Subscribe(OnDestroyCuttingFruit);
        _eventBus.GetEvent<DestroyCuttingFruitEvent<SuperFruitVm>>().Subscribe(OnDestroyCuttingFruit);
        _eventBus.GetEvent<FruitCutEvent<FruitVm>>().Subscribe(OnFruitCut);
        _eventBus.GetEvent<FruitCutEvent<SuperFruitVm>>().Subscribe(OnFruitCut);

        _eventBus.GetEvent<DestroyGameObjectEvent<BombVm>>().Subscribe(OnDestroyBomb);

        _eventBus.GetEvent<ExplodedBombEvent<BombVm>>().Subscribe(OnBombExploded);
    }

    
    [Inject] public void Construct(IEventBus eventBus, IGameDataModel gameDataModel)
    {
        _eventBus = eventBus;
        _gameDataModel = gameDataModel;

        InitGameObjects();
        InitBusEvents();
    }
}
