using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class MainMenuDataModel : MonoBehaviour, IMainMenuDataModel
{
    private EnumMenuState _state;        //текущее игровое состояния

    public IEventBus _eventBus;

    [Inject]
    public void Construct(IEventBus eventBus)
    {
        Debug.Log("Конструктор MenuDataModel");
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
