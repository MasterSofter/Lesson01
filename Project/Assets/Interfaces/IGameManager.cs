using UnityEngine;

public abstract class IGameManager : MonoBehaviour
{
    //Обработчики событий для обновления логики игры
    public abstract void OnUpdateState(EnumState newState);
    public abstract void OnUdpateCurrentGameTime(float seconds);
    public abstract void OnUpdateCountMisses(int newCountMisses);

    // Обработчик события, вызываемый при уничтожении фрукта
    public abstract void OnDestroyFruit(int score);
    public abstract void OnDestroyMissedFruit();
    public abstract void OnCutSuperFruit(GameObject cutSuperFruit);
    public abstract void OnDestroyBomb();
    public abstract void OnClickSuperFruit(int score, Vector3 camerafocusPosition);
    public abstract void OnDestroySuperFruit();
    

    public abstract void OnBeginStartGameAnimation();
    public abstract void OnFinishStartGameAnimation();

    public abstract void OnBeginResumeGameAnimation();
    public abstract void OnFinishResumeGameAnimation();

    public abstract void OnBeginPauseGameAnimation();
    public abstract void OnFinishPauseGameAnimation();

    public abstract void OnBeginReturnMainMenuAnimation();
    public abstract void OnFinishReturnMainMenuAnimation();

}
