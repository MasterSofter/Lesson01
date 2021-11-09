using System.Collections;
using UnityEngine;
using EventBus.Interfaces;
using Zenject;

public class MainMenuViewer : MonoBehaviour, IMainMenuViewer
{
    private IEventBus _eventBus;
    [SerializeField] private Animator _oldManAnimator;
    [SerializeField] private Animator _logoAnimator;
    [SerializeField] private Animator _aboutButtonAnimator;
    [SerializeField] private Animator _aboutWindowAnimator;


    public void OnChangedStateMenu(EnumMenuState enumMenuState) {
        switch (enumMenuState)
        {
            case EnumMenuState.StartMenu:
                //_eventBus.GetEvent<MenuStartEvent>().Publish(new MenuStartEventArg(false));
                StartCoroutine(ShowStartMenuAnimation());
                break;
            case EnumMenuState.ExitMenu:
                StartCoroutine(ShowExitMenuAnimation());
                break;

        }
    }

    /******************************************************************
     *              Обработчики событий нажатия на кнопку UI
     ******************************************************************/

    public void OnShowAbout() => StartCoroutine(ShowAboutWindow());
    public void OnHideAbout() => StartCoroutine(HideAboutWindow());

    /******************************************************************
    *                             Корутины
    ******************************************************************/

    public IEnumerator ShowStartMenuAnimation() {
        _eventBus.GetEvent<MenuStartEvent>().Publish(new MenuStartEventArg(true));

        _logoAnimator.Play("LogoAnimation");
        while (!_logoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Showed_LogoAnimation"))
            yield return new WaitForEndOfFrame();

        _oldManAnimator.Play("Show_OldManAnimation");
        while (!_oldManAnimator.GetCurrentAnimatorStateInfo(0).IsName("Showed_OldManAnimation"))
            yield return new WaitForEndOfFrame();

        _aboutButtonAnimator.Play("ShowAboutButtonAnimation");
        while (!_aboutButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Showed_AboutButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _eventBus.GetEvent<MenuStartEvent>().Publish(new MenuStartEventArg(false));
    }

    public IEnumerator ShowAboutWindow() {
        _aboutButtonAnimator.Play("HideAboutButtonAnimation");
        while (!_aboutButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hided_AboutButtonAnimation"))
            yield return new WaitForEndOfFrame();

        _aboutWindowAnimator.Play("Show_MenuAnimation");
        while (!_aboutWindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitShowed_MenuAnimation"))
            yield return new WaitForEndOfFrame();  
    }

    public IEnumerator HideAboutWindow() {
        _aboutWindowAnimator.Play("Hide_MenuAnimation");
        while (!_aboutWindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitHided_MenuAnimation"))
            yield return new WaitForEndOfFrame();

        _aboutButtonAnimator.Play("ShowAboutButtonAnimation");
        while (!_aboutButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Showed_AboutButtonAnimation"))
            yield return new WaitForEndOfFrame();
    }

    public IEnumerator ShowExitMenuAnimation()
    {
        _eventBus.GetEvent<MenuExitEvent>().Publish(new MenuExitEventArg(true));
        yield return new WaitForEndOfFrame();
        _eventBus.GetEvent<MenuExitEvent>().Publish(new MenuExitEventArg(false));
    }


    private void InitBusEvents() => _eventBus.GetEvent<MenuDataModelChangedState<EnumMenuState>>().Subscribe(OnChangedStateMenu);

    [Inject]
    public void Construct(IEventBus eventBus)
    {
        Debug.Log("Конструктор MenuViewer");
        _eventBus = eventBus;
        InitBusEvents();
    }
}
