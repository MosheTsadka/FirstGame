using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    public static int SpawnCount { get; private set; }

    [SerializeField] private GameObject obstacle;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private int poolSize;

    private List<GameObject> _obstaclesPool;
    private int _activeSpawnPointCount;
    private int _obstaclesPerSpawn;

    private void Init()
    {
        _activeSpawnPointCount = 1;
        _obstaclesPerSpawn = 1;

        if (_obstaclesPool != null)
        {
            _obstaclesPool.Clear();
        }
        else
        {
            _obstaclesPool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstacle);
            obj.SetActive(false);
            _obstaclesPool.Add(obj);
        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        spawnRate -= Time.deltaTime;

        if (!(spawnRate <= 0f)) return;
        StartCoroutine(SpawnObstaclesCoroutine()); // מפעיל את הספאון

        SpawnCount++; // סופר כמה ספאונים כבר היו

        switch (SpawnCount)
        {
            // ⏱️ קיצור הזמן בין ספאונים
            case 15:
                timeBetweenSpawns = 0.8f;
                break;
            case 30:
                timeBetweenSpawns = 0.6f;
                break;
            case 50:
                timeBetweenSpawns = 0.4f;
                break;
            // 🔢 הגדלת מספר האובייקטים בכל ספאון
            case 20:
                _obstaclesPerSpawn = 2;
                break;
            case 35:
                _obstaclesPerSpawn = 3;
                break;
            // 📍 הגדלת מספר נקודות הספאון הפעילות
            case 10:
                _activeSpawnPointCount = 2;
                break;
            case 25:
                _activeSpawnPointCount = 3;
                break;
        }

        // ⏱️ איפוס הטיימר לספאון הבא
        spawnRate = timeBetweenSpawns;
    }


    /*private void Spawn()
    {
        List<int> availableIndexes = new List<int>();

        for (int i = 0; i < _activeSpawnPointCount; i++)
        {
            availableIndexes.Add(i);
        }

        int spawnAmount = Mathf.Min(_obstaclesPerSpawn, availableIndexes.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, availableIndexes.Count);
            int chosenSpawnPiont = availableIndexes[randomIndex];

            Instantiate(obstacle, spawnPosition[chosenSpawnPiont].position, Quaternion.identity);

            availableIndexes.RemoveAt(randomIndex);
        }
    }*/

    private GameObject GetPooledObstacle()
    {
        foreach (var obj in _obstaclesPool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        return null;
    }

    private IEnumerator SpawnObstaclesCoroutine()
    {
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < _activeSpawnPointCount; i++)
            availableIndexes.Add(i);

        for (int i = 0; i < _obstaclesPerSpawn; i++)
            if (availableIndexes.Count == 0)
                break;

        int randomIndex = Random.Range(0, availableIndexes.Count);
        int spawnIndex = availableIndexes[randomIndex];
        availableIndexes.RemoveAt(randomIndex);

        GameObject obstacleObj = GetPooledObstacle();
        if (obstacleObj != null)
        {
            obstacleObj.transform.position = spawnPosition[spawnIndex].position;
            obstacleObj.SetActive(true);
        }

        yield return new WaitForSeconds(0.2f);
    }
}