using UnityEngine;
using EventBus.Interfaces;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameApplication : MonoBehaviour
{
    private IEventBus _eventBus;

    [SerializeField] private GameViewer _gameViewer;

    private GameManager _gameManager;
    private GameDataModel _gameDataModel;

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

    private void Awake()
    {
        _eventBus = new EventBus.Composite.Events.EventBus();
        _eventBus.GetEvent<ButtonClickLoadSceneEvent>().Subscribe(OnLoadScene);


        _gameDataModel = new GameDataModel(_eventBus);
        _gameViewer.Init(_eventBus, _gameDataModel);
        _gameManager = new GameManager(_eventBus, _gameViewer, _gameDataModel);

        StartCoroutine(TimeTick());
    }
}
