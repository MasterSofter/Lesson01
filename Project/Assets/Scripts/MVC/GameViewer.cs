using System;
using System.Collections;
using UnityEngine;

public class GameViewer : IGameViewer
{
    private EnumState _currentState;

    [SerializeField] private IGameDataModel _gameDataModel;
    [SerializeField] private TMPro.TMP_Text _textTime;
    [SerializeField] private TMPro.TMP_Text _textGameScore;

    [SerializeField] private Animator _pauseButtonAnimator;
    [SerializeField] private Animator _pauseMenuAnimator;
    [SerializeField] private Animator _mainMenuAnimator;
    [SerializeField] private Animator _gameOverMenuAnimation;
    [SerializeField] private Animator _aboutAutorMenuAnimator;
    [SerializeField] private Animator _hideAboutAutorButtonAnimator;

    public override event Action<GameObject> OnCameraFocus;
    public override event Action<GameObject> OnCameraDefocus;

    public override event Action OnBeginStartGameAnimation;
    public override event Action OnFinishStartGameAnimation;

    public override event Action OnBeginResumeGameAnimation;
    public override event Action OnFinishResumeGameAnimation;

    public override event Action OnBeginPauseGameAnimation;
    public override event Action OnFinishPauseGameAnimation;

    public override event Action OnBeginReturnMainMenuAnimation;
    public override event Action OnFinishReturnMainMenuAnimation;

    public override event Action OnBeginLoadGameAnimation;
    public override event Action OnFinishLoadGameAnimation;


    public override event Action OnBeginGameOverAnimation;
    public override event Action OnFinishGameOverAnimation;


    public override void OnUpdateState(EnumState newState){
        _currentState = newState;
        if (newState == EnumState.LoadGame) StartCoroutine(LoadGame());
        if (newState == EnumState.GameOver) StartCoroutine(GameOver());
    }
    public override void OnUdpateCurrentGameTime(float seconds){
        if (seconds == 0) { _textTime.text = "00:00"; return; }

        int minutes = (int)seconds / 60;
        int _seconds = (int)seconds - minutes * 60;

        string secondsStr = _seconds >= 10 ? _seconds.ToString() : $"0{_seconds}";
        string minitesStr = minutes >= 10 ? minutes.ToString() : $"0{minutes}";

        _textTime.text = $"{minitesStr}:{secondsStr}";
    }
    public override void OnUpdateCountGameScore(int newCountGameScore) => _textGameScore.text = newCountGameScore.ToString();
    public override void OnUpdateCountMisses(int newCountMisses) { }

    public void OnGameStartButtonClick() => StartCoroutine(StartGame());
    public void OnGamePauseButtonClick() => StartCoroutine(PauseGame());
    public void OnGameResumeButtonClick() => StartCoroutine(ResumeGame());
    public void OnGameRestartButtonClick() => StartCoroutine(RestartGame());
    public void OnReturnMainMenuButtonClick() => StartCoroutine(ReturnMainMenu());
    public void OnShowAboutAutorButtonClick() => StartCoroutine(ShowAboutAutor());
    public void OnHideAboutAutorButtonClick() => StartCoroutine(HideAboutAutor());


    public IEnumerator LoadGame()
    {
        OnBeginLoadGameAnimation?.Invoke();

        _mainMenuAnimator.Play("Show_MenuAnimation");
        while (!_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishLoadGameAnimation?.Invoke();
    }
    public IEnumerator StartGame()
    {
        OnBeginStartGameAnimation?.Invoke();

        _mainMenuAnimator.Play("Hide_MenuAnimation");
        while (!_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _pauseButtonAnimator.Play("Show_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_ShowedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishStartGameAnimation?.Invoke();
    }
    public IEnumerator GameOver() { 
        OnBeginGameOverAnimation?.Invoke();

        _pauseButtonAnimator.Play("Hide_ButtonAnimation");

        _gameOverMenuAnimation.Play("Show_MenuAnimation");
        while (!_gameOverMenuAnimation.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishGameOverAnimation?.Invoke();
    }
    public IEnumerator RestartGame()
    {

        OnBeginStartGameAnimation?.Invoke();
        if (_currentState == EnumState.GameOver)
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

        OnFinishStartGameAnimation?.Invoke();
    }
    public IEnumerator ResumeGame()
    {
        OnBeginResumeGameAnimation?.Invoke();

        _pauseMenuAnimator.Play("Hide_MenuAnimation");
        while (!_pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _pauseButtonAnimator.Play("Show_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_ShowedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishResumeGameAnimation?.Invoke();

    }
    public IEnumerator PauseGame()
    {
        OnBeginPauseGameAnimation?.Invoke();

        _pauseButtonAnimator.Play("Hide_ButtonAnimation");
        while (!_pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait_HidedButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _pauseMenuAnimator.Play("Show_MenuAnimation");
        while (!_pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishPauseGameAnimation?.Invoke();
    }

    public IEnumerator ReturnMainMenu()
    {
        OnBeginReturnMainMenuAnimation?.Invoke();

        if (_currentState == EnumState.GameOver)
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



        _mainMenuAnimator.Play("Show_MenuAnimation");
        while (!_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        OnFinishReturnMainMenuAnimation?.Invoke();
    }

    public IEnumerator ShowAboutAutor() {
        _mainMenuAnimator.Play("Hide_MenuAnimation");
        while (!_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _aboutAutorMenuAnimator.Play("Show_MenuAnimation");
        while (!_aboutAutorMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _hideAboutAutorButtonAnimator.Play("Show_ButtonAnimation");
    }
    public IEnumerator HideAboutAutor(){

        _hideAboutAutorButtonAnimator.Play("Hide_ButtonAnimation");

        _aboutAutorMenuAnimator.Play("Hide_MenuAnimation");
        while (!_aboutAutorMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _mainMenuAnimator.Play("Show_MenuAnimation");
        while (!_mainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();
    }

    private void Awake()
    {
        _gameDataModel.OnChangeState += OnUpdateState;
        _gameDataModel.OnChangeCurrentGameTime += OnUdpateCurrentGameTime;
        _gameDataModel.OnChangedCountGameScore += OnUpdateCountGameScore;
        _gameDataModel.OnChangedCountMisses += OnUpdateCountMisses;
    }
}
