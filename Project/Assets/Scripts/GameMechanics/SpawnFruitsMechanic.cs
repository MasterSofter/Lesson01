using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruitsMechanic : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;            //массив точек, в которых можно создать обьект

    public GameObject spawnObject;
    public float waitingTimeBeforeSpawn = 2;    //время ожидания между созданием объектов

    private float startForce = 18;
    private float startTorgue = 18;
    
    private int array_spawnPointsLength = 0;

    private void OnEnable()
    {
        StartCoroutine(SpawnFruits());
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {
        array_spawnPointsLength = spawnPoints.Length;
    }


    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            //выбираем рандомную точку для спавна
            int spawnIndexOfTransormPoint = Random.Range(0, array_spawnPointsLength);
            Transform spawnPoint = spawnPoints[spawnIndexOfTransormPoint];

            if (MainGameManager.Instance != null)
            {
                GameObject newObj = MainGameManager.Instance.AddGameObject(spawnObject, spawnPoint.position, spawnPoint.rotation);
                newObj.GetComponent<Rigidbody2D>().AddForce(newObj.transform.up * startForce, ForceMode2D.Impulse);
                newObj.GetComponent<Rigidbody2D>().AddTorque(startTorgue);
            }
            yield return new WaitForSeconds(waitingTimeBeforeSpawn);
        }
    }
}

