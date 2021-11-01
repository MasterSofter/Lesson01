using EventBus.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuApplication : MonoBehaviour
{
    private IEventBus _eventBus;

    [SerializeField] private MenuViewer _menuViewer;
    [SerializeField] private ButtonPlayClassicGame _buttonPlayClassicGame;

    private MenuManager _menuManager;
    private MenuDataModel _menuDataModel;

    public void OnLoadScene(EnumScene enumScene) {
        switch (enumScene) {
            case EnumScene.ClassicGameScene:
                SceneManager.LoadScene("ClassicGameScene");
                break;
            case EnumScene.ArcadeGameScene:
                SceneManager.LoadScene("ArcadeGameScene");
                break;
        }
    }

    private void Awake()
    {
        _eventBus = new EventBus.Composite.Events.EventBus();
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Subscribe(OnLoadScene);

        _buttonPlayClassicGame.Init(_eventBus);
        _menuViewer.Init(_eventBus);

        _menuDataModel = new MenuDataModel(_eventBus);
        _menuManager = new MenuManager(_eventBus, _menuViewer, _menuDataModel);


    }
}
