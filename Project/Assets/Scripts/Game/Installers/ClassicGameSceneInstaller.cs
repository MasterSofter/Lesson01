using UnityEngine;
using EventBus.Interfaces;
using Zenject;

public class ClassicGameSceneInstaller : MonoInstaller
{
    private IEventBus _eventBus;
    [SerializeField] ClassicGameViewer _gameViewer;
    [SerializeField] ClassicGameManager _gameManager;
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