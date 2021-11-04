public interface IGameManager
{
    void OnGameStart(GameStartEventArg arg);
    void OnResumeGame(GameResumeEventArg arg);
    void OnPauseGame(GamePauseEventArg arg);
    void OnTimeTick(TimeTickEventArg arg);
}
