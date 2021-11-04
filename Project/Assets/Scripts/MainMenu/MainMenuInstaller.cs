using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    private IEventBus _eventBus;
    [SerializeField] MainMenuManager _mainMenuManager;
    [SerializeField] MainMenuViewer _mainMenuViewer;
    [SerializeField] MainMenuDataModel _mainMenuDataModel;

    public override void InstallBindings()
    {
        _eventBus = new EventBus.Composite.Events.EventBus();

        Container.Bind<IEventBus>().FromInstance(_eventBus);
        Container.Bind<IMainMenuManager>().FromInstance(_mainMenuManager);
        Container.Bind<IMainMenuViewer>().FromInstance(_mainMenuViewer);
        Container.Bind<IMainMenuDataModel>().FromInstance(_mainMenuDataModel);
    }
}