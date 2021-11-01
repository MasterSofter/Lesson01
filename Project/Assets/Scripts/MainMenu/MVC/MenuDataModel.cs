using EventBus.Interfaces;
using UnityEngine;

public class MenuDataModel : MonoBehaviour
{
    private EnumMenuState _state;        //текущее игровое состояния

    public IEventBus _eventBus;

    public MenuDataModel(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public EnumMenuState MenuState
    {
        get { return _state; }
        set
        {
            _state = value;
            _eventBus.GetEvent<MenuDataModelChangedState<EnumMenuState>>().Publish(_state);
        }
    }
}
