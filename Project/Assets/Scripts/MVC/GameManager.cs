using System.Collections;
using UnityEngine;

public class GameManager : IGameManager
{
    private EnumState _currentState;

    [SerializeField] private IGameDataModel _gameDataModel;
    [SerializeField] private IGameViewer _gameViewer;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private DestroyManager2D _destroyManager;

    public override void OnUpdateState(EnumState newState) => _currentState = newState;
    public override void OnUdpateCurrentGameTime(float seconds) { }
    public override void OnUpdateCountMisses(int newCountMisses) { }

    public override void OnDestroyFruit(int score){
        if(_currentState == EnumState.Game)
            _gameDataModel.AddScore(score);
    }
    public override void OnDestroyMissedFruit(){
        if (_currentState == EnumState.Game)
            _gameDataModel.ChangeState(EnumState.GameOver);
    }
    public override void OnDestroyBomb()
    {
        if (_currentState == EnumState.Game)
            _gameDataModel.ChangeState(EnumState.GameOver);

    }
    public override void OnClickSuperFruit(int score, Vector3 camerafocusPosition)
    {
        if (_currentState == EnumState.Game)
        {
            _gameDataModel.AddScore(score);
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
    public override void OnDestroySuperFruit()
    {
        _gameDataModel.ChangeState(EnumState.Game);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public override void OnCutSuperFruit(GameObject cutSuperFruit) { }


    public override void OnBeginStartGameAnimation() {
        _gameDataModel.ChangeTime(0);
        _gameDataModel.ChangeCountGameScore(0);
    }
    public override void OnFinishStartGameAnimation() => _gameDataModel.ChangeState(EnumState.Game);


    public override void OnBeginResumeGameAnimation(){}
    public override void OnFinishResumeGameAnimation() => _gameDataModel.ChangeState(EnumState.Game);

    public override void OnBeginPauseGameAnimation() => _gameDataModel.ChangeState(EnumState.Pause);
    public override void OnFinishPauseGameAnimation() {}


    public override void OnBeginReturnMainMenuAnimation(){
        _gameDataModel.ChangeTime(0);
        _gameDataModel.ChangeCountGameScore(0);
        
    }
    public override void OnFinishReturnMainMenuAnimation() => _gameDataModel.ChangeState(EnumState.MainMenu);

  
    private IEnumerator SpawnBomb()
    {
        while (true)
        {
            if (_currentState == EnumState.Game)
                _spawner.SpawnBomb().GetComponent<Bomb>().OnDestroyBomb += OnDestroyBomb;
            yield return new WaitForSeconds(5);
        }

    }
    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            if(_currentState == EnumState.Game)
                _spawner.SpawnFruit().GetComponent<Fruit>().OnDestroyFruit += OnDestroyFruit;
            yield return new WaitForSeconds(1.2f);
        }
       
    }
    private IEnumerator SpawnSuperFruits() {

        while (true)
        {
            if (_currentState == EnumState.Game) {
                SuperFruit superFruit = _spawner.SpawnSuperFruit().GetComponent<SuperFruit>();
                superFruit.OnDestroySuperFruit += OnDestroySuperFruit;
                superFruit.OnClickSuperFruit += OnClickSuperFruit;
            }
                
            yield return new WaitForSeconds(12f);
        }
    }
    private IEnumerator UpdateGameTime(){
        _gameDataModel.ChangeState(EnumState.LoadGame);
        while (true)
        {
            if (_currentState == EnumState.Game)
                _gameDataModel.AddTime(Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Awake()
    {
        _gameDataModel.OnChangeState += OnUpdateState;
        _gameDataModel.OnChangeCurrentGameTime += OnUdpateCurrentGameTime;
        _gameDataModel.OnChangedCountMisses += OnUpdateCountMisses;

        _gameViewer.OnBeginStartGameAnimation += OnBeginStartGameAnimation;
        _gameViewer.OnFinishStartGameAnimation += OnFinishStartGameAnimation;

        _gameViewer.OnBeginResumeGameAnimation += OnBeginResumeGameAnimation;
        _gameViewer.OnFinishResumeGameAnimation += OnFinishResumeGameAnimation;

        _gameViewer.OnBeginPauseGameAnimation += OnBeginPauseGameAnimation;
        _gameViewer.OnFinishPauseGameAnimation += OnFinishPauseGameAnimation;

        _gameViewer.OnBeginReturnMainMenuAnimation += OnBeginReturnMainMenuAnimation;
        _gameViewer.OnFinishReturnMainMenuAnimation += OnFinishReturnMainMenuAnimation;

        _destroyManager.OnDestroyMissedFruit += OnDestroyMissedFruit;

        StartCoroutine(SpawnBomb());
        StartCoroutine(SpawnFruits());
        StartCoroutine(SpawnSuperFruits());
        StartCoroutine(UpdateGameTime());
    }



}
