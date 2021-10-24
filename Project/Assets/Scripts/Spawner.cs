using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _fruitsPrefubs;
    [SerializeField] private GameObject[] _superFruitsPrefubs;
    [SerializeField] private GameObject _bombPrefub;


    private float _startForce = 18;
    private float _startTorgue = 22;

    private int _array_spawnPointsLength = 0;
    private int _array_fruitsLength = 0;
    private int _array_superFruitsLength = 0;

    public GameObject SpawnSuperFruit() {
        int rundomIndexSpawnPoint = Random.Range(0, _array_spawnPointsLength);
        int randowIndexFruit = Random.Range(0, _array_superFruitsLength);

        GameObject newGameObj = Instantiate(_superFruitsPrefubs[randowIndexFruit], _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startTorgue);

        return newGameObj;
    }
    public GameObject SpawnFruit(){
        int rundomIndexSpawnPoint = Random.Range(0, _array_spawnPointsLength);
        int randowIndexFruit = Random.Range(0, _array_fruitsLength);

        GameObject newGameObj = Instantiate(_fruitsPrefubs[randowIndexFruit], _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startTorgue);

        return newGameObj;
    }
    public GameObject SpawnBomb()
    {
        int rundomIndexSpawnPoint = Random.Range(0, _array_spawnPointsLength);

        GameObject newGameObj = Instantiate(_bombPrefub, _spawnPoints[rundomIndexSpawnPoint].position, this.transform.rotation);
        newGameObj.GetComponent<Rigidbody2D>().AddForce(_spawnPoints[rundomIndexSpawnPoint].transform.up * _startForce, ForceMode2D.Impulse);
        newGameObj.GetComponent<Rigidbody2D>().AddTorque(_startTorgue);

        return newGameObj;

    }

    private void Awake()
    {
        _array_spawnPointsLength = _spawnPoints.Length;
        _array_fruitsLength = _fruitsPrefubs.Length;
        _array_superFruitsLength = _superFruitsPrefubs.Length;

    }
}
