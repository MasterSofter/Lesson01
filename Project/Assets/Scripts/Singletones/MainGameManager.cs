using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set;} // Экземпляр объекта
   

    [SerializeField]
    private SpawnFruitsMechanic spawnFruits;

    private bool isSlowMode = false;
    private float countLoops = 0;
    private float currentTime = 0;
    private float timeSpawn = 2f;
    private float timePauseSpawn = 2f;
    private float waitingTimeBeforeSpawn = 1f;
    private List<GameObject> gameObjects = new List<GameObject>();

    private enum GameState {
        Game = 0,
        Pause = 1,
        GameOver
    }

    private GameState currentState;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        currentState = GameState.Game;
        StartCoroutine(GameLoop());
    }

    public void GameOver() {
        currentState = GameState.GameOver;
    }

    // Функция замедляет 
    public void SlowDownSpeedGamePhysic() {
        isSlowMode = true;
        foreach (var item in gameObjects) {
            Rigidbody2D rb2d = item.GetComponent<Rigidbody2D>();
            if (rb2d == null)
            {
                Rigidbody[] listRigidBodies = item.GetComponentsInChildren<Rigidbody>();
                foreach (var jtem in listRigidBodies)
                    jtem.drag = 30;
            }
            else
                rb2d.drag = 30;

        } 
    }

    public void ReturnDefaultSpeedGamePhysic()
    {
        isSlowMode = false;
        foreach (var item in gameObjects)
        {
            Rigidbody2D rb2d = item.GetComponent<Rigidbody2D>();
            if (rb2d == null)
            {
                Rigidbody[] listRigidBodies = item.GetComponentsInChildren<Rigidbody>();
                foreach (var jtem in listRigidBodies)
                    jtem.drag = 0;
            }
            else
                rb2d.drag = 0;

        }
    }

    private IEnumerator GameLoop()
    {
        while(currentState == GameState.Game)
        {
            if (isSlowMode)
                yield return new WaitForSeconds(4);

            timeSpawn += 0.4f;

            yield return new WaitForSeconds(timePauseSpawn);

            
            spawnFruits.waitingTimeBeforeSpawn = waitingTimeBeforeSpawn;

            while (currentTime < timeSpawn)
            {
                int randomIndex;

                if (countLoops == 8)
                    spawnFruits.spawnObject = GameContextManager.Instance.bomb;
                else if (countLoops == 6)
                {
                    randomIndex = Random.Range(0, GameContextManager.Instance.superFruits.Length);
                    spawnFruits.spawnObject = GameContextManager.Instance.superFruits[randomIndex];
                }
                else
                {
                    randomIndex = Random.Range(0, GameContextManager.Instance.fruits.Length);
                    spawnFruits.spawnObject = GameContextManager.Instance.fruits[randomIndex];
                }

                spawnFruits.enabled = true;
                currentTime += Time.deltaTime;
            }

            countLoops++;
            spawnFruits.enabled = false;
            currentTime = 0;

            if (waitingTimeBeforeSpawn > 0.4f)
                waitingTimeBeforeSpawn -= 0.2f;
            if (timePauseSpawn > 0.4f)
                timePauseSpawn -= 0.2f;

            if (countLoops >= 10)
            {
                countLoops = 0;
                yield return new WaitForSeconds(2);
            }    


        }

        if(currentState == GameState.GameOver)
        {
            UIManager.Instance.ShowGameOver();
            StopAllCoroutines();
        }
 
    }

    public GameObject AddGameObject(GameObject gameObjectPrefub, Vector3 position, Quaternion rotation) {
        GameObject newObj = Instantiate(gameObjectPrefub, position, rotation);
        gameObjects.Add(newObj);
        return newObj;
    }

    public void DestroyGameObject(GameObject _gameObject) {
        gameObjects.Remove(_gameObject);
        Destroy(_gameObject);
    }
}
