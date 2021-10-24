using System;
using UnityEngine;

public abstract class IGameViewer : MonoBehaviour
{
    public abstract event Action<GameObject> OnCameraFocus;
    public abstract event Action<GameObject> OnCameraDefocus;

    public abstract event Action OnBeginStartGameAnimation;
    public abstract event Action OnFinishStartGameAnimation;

    public abstract event Action OnBeginResumeGameAnimation;
    public abstract event Action OnFinishResumeGameAnimation;

    public abstract event Action OnBeginPauseGameAnimation;
    public abstract event Action OnFinishPauseGameAnimation;

    public abstract event Action OnBeginReturnMainMenuAnimation;
    public abstract event Action OnFinishReturnMainMenuAnimation;

    public abstract event Action OnBeginLoadGameAnimation;
    public abstract event Action OnFinishLoadGameAnimation;


    public abstract event Action OnBeginGameOverAnimation;
    public abstract event Action OnFinishGameOverAnimation;


    //Обработчики событий для обновления пользовательского интерфейса
    public abstract void OnUpdateState(EnumState newState);
    public abstract void OnUdpateCurrentGameTime(float seconds);

    public abstract void OnUpdateCountGameScore(int newCountGameScore);
    public abstract void OnUpdateCountMisses(int newCountMisses);
}
