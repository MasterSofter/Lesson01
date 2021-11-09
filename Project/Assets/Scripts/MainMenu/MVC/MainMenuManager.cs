using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class MainMenuManager : MonoBehaviour, IMainMenuManager
{
    private IEventBus _eventBus;
    private IMainMenuDataModel _menuDataModel;

    public void OnMenuStart(MenuStartEventArg arg)
    {
        if (arg.Started == false)
            _menuDataModel.MenuState = EnumMenuState.Menu;
    }
    [Inject]
    public void Construct(IEventBus eventBus, IMainMenuDataModel menuDataModel)
    {
        Debug.Log("Конструктор MenuManager");
        _eventBus = eventBus;
        _eventBus.GetEvent<MenuStartEvent>().Subscribe(OnMenuStart);
        _menuDataModel = menuDataModel;

        _menuDataModel.MenuState = EnumMenuState.StartMenu;
    }

}
