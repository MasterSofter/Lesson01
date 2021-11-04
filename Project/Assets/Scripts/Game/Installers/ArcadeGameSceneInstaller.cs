using UnityEngine;
using EventBus.Interfaces;
using Zenject;

public class ArcadeGameSceneInstaller : MonoInstaller
{
    private IEventBus _eventBus;
    [SerializeField] ArcadeGameViewer _gameViewer;
    [SerializeField] ArcadeGameManager _gameManager;
    [SerializeField] GameDataModel _gameDataModel;

    public override void InstallBindings()
    {
        _eventBus = new EventBus.Composite.Events.EventBus();

        Container.Bind<IEventBus>().FromInstance(_eventBus);
        Container.Bind<IGameManager>().FromInstance(_gameManager);
        Container.Bind<IGameViewer>().FromInstance(_gameViewer);
        Container.Bind<IGameDataModel>().FromInstance(_gameDataModel);
    }
}