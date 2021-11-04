using EventBus.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuApplication : MonoBehaviour
{
    private IEventBus _eventBus;

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

    [Inject]
    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Subscribe(OnLoadScene);
    }
}
