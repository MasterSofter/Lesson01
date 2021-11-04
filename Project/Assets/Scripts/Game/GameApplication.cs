using UnityEngine;
using EventBus.Interfaces;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;

public class GameApplication : MonoBehaviour
{
    private IEventBus _eventBus;

    private IEnumerator TimeTick() {
        while (true) {
            yield return new WaitForSeconds(1);
            _eventBus.GetEvent<TimeTickEvent>().Publish(new TimeTickEventArg(1));
        }
    }

    public void OnLoadScene(EnumScene enumScene)
    {
        switch (enumScene)
        {
            case EnumScene.MainMenuScene:
                SceneManager.LoadScene("MainMenuScene");
                break;
        }
    }

    [Inject] public void Construct(IEventBus eventBus) {
        _eventBus = eventBus;
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Subscribe(OnLoadScene);
        StartCoroutine(TimeTick());
    } 
}
