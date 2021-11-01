using EventBus.Interfaces;
public class MenuManager
{
    private IEventBus _eventBus;
    private MenuViewer _menuViewer;
    private MenuDataModel _menuDataModel;

    public void OnMenuStart(MenuStartEventArg arg)
    {
        if (arg.Started == false)
            _menuDataModel.MenuState = EnumMenuState.Menu;
    }

    public MenuManager(IEventBus eventBus, MenuViewer menuViewer, MenuDataModel menuDataModel)
    {
        _eventBus = eventBus;
        _eventBus.GetEvent<MenuStartEvent>().Subscribe(OnMenuStart);


        _menuViewer = menuViewer;
        _menuDataModel = menuDataModel;

        _menuDataModel.MenuState = EnumMenuState.StartMenu;
    }

}
