using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    /*public static int SpawnCount { get; private set; }

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

    private void Awake()
    {
        
    }

    void Start()
    {
        
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
        // Create a list of available spawn point indexes
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < _activeSpawnPointCount; i++)
            availableIndexes.Add(i);

        // Spawn the defined number of obstacles
        for (int i = 0; i < _obstaclesPerSpawn; i++)
        {
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

            // Wait a bit between spawns for rain-like effect
            yield return new WaitForSeconds(0.2f);
        }
    }*/
    
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float initialTimeBetweenSpawns = 1f;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Speed Settings")]
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float speedMultiplier = 0.6f;
    [SerializeField] private float maxSpeed = 7f;

    private List<GameObject> _obstaclesPool;
    private float _spawnTimer;
    private int _activeSpawnPointCount = 1;
    private int _obstaclesPerSpawn = 1;
    private bool _spawning = false;
    private int _spawnCount = 0;
    private float _currentTimeBetweenSpawns;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart += Init;
            GameManager.Instance.OnGameRestart += Init;
            GameManager.Instance.OnGameOver += StopSpawning;
        }
        else
        {
            Debug.LogWarning("Spawner awakened before GameManager was initialized.");
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= Init;
            GameManager.Instance.OnGameRestart -= Init;
            GameManager.Instance.OnGameOver -= StopSpawning;
        }
    }

    private void Init()
    {
        _activeSpawnPointCount = 1;
        _obstaclesPerSpawn = 1;
        _spawnCount = 0;
        _currentTimeBetweenSpawns = initialTimeBetweenSpawns;
        _spawnTimer = _currentTimeBetweenSpawns;

        if (_obstaclesPool == null)
            _obstaclesPool = new List<GameObject>();
        else
        {
            foreach (var obj in _obstaclesPool)
                obj.SetActive(false);
            _obstaclesPool.Clear();
        }

        // Fill the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            _obstaclesPool.Add(obj);
        }

        _spawning = true;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!_spawning) return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            StartCoroutine(SpawnObstaclesCoroutine());
            _spawnCount++;

            // Example: scaling difficulty (אפשר להרחיב)
            switch (_spawnCount)
            {
                case 15: _currentTimeBetweenSpawns = 0.8f; break;
                case 30: _currentTimeBetweenSpawns = 0.6f; break;
                case 50: _currentTimeBetweenSpawns = 0.4f; break;
                case 20: _obstaclesPerSpawn = 2; break;
                case 35: _obstaclesPerSpawn = 3; break;
                case 14: _activeSpawnPointCount = 2; break;
                case 29: _activeSpawnPointCount = 3; break;
            }

            _spawnTimer = _currentTimeBetweenSpawns;
        }
    }

    private void StopSpawning()
    {
        _spawning = false;
        StopAllCoroutines();
    }

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
        for (int i = 0; i < _activeSpawnPointCount && i < spawnPositions.Length; i++)
            availableIndexes.Add(i);

        for (int i = 0; i < _obstaclesPerSpawn; i++)
        {
            if (availableIndexes.Count == 0) break;

            int randomIndex = Random.Range(0, availableIndexes.Count);
            int spawnIndex = availableIndexes[randomIndex];
            availableIndexes.RemoveAt(randomIndex);

            GameObject obstacleObj = GetPooledObstacle();
            if (obstacleObj != null)
            {
                obstacleObj.transform.position = spawnPositions[spawnIndex].position;
                // *** Speed is calculated ONCE per spawn, not updated later ***
                float calculatedSpeed = baseSpeed + Mathf.Log(1 + _spawnCount) * speedMultiplier;
                calculatedSpeed = Mathf.Min(calculatedSpeed, maxSpeed);

                var obstacleScript = obstacleObj.GetComponent<ObstacleBehaviour>();
                if (obstacleScript != null)
                {
                    obstacleScript.SetSpeed(calculatedSpeed);
                }

                obstacleObj.SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}