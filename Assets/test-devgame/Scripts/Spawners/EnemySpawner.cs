using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;  // Префаб врага
    [Range(0f, 100f)]
    public float spawnProbability;  // Вероятность спавна данного префаба в процентах
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> enemiesToSpawn; // Список данных врагов для спавна
    [SerializeField] private Transform characterTransform; // Трансформ игрока
    [SerializeField] private float initialSpawnInterval = 2f; // Начальный интервал между спавнами
    [SerializeField] private float spawnIntervalDecrease = 0.1f; // Уменьшение интервала
    [SerializeField] private float minSpawnInterval = 0.5f; // Минимальный интервал
    
    [SerializeField] private float minSpawnOffset = 2f; // Минимальный офсет от границ камеры
    [SerializeField] private float maxSpawnOffset = 5f; // Максимальный офсет от границ камеры

    private Camera _mainCamera;
    private float _spawnTimer;
    private float _currentSpawnInterval;
    private float _timeElapsed;

    // Переменные для отслеживания количества спавнов
    //private Dictionary<GameObject, int> _spawnCount = new Dictionary<GameObject, int>();
    //private int _totalSpawnCount = 0;

    private void Awake()
    {
        _mainCamera = Camera.main; // Кешируем mainCamera

        /*// Инициализируем счетчики
        foreach (var enemy in enemiesToSpawn)
        {
            _spawnCount[enemy.enemyPrefab] = 0;
        }*/
    }

    private void Start()
    {
        _currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating(nameof(DecreaseSpawnInterval), 10f, 10f);
        //InvokeRepeating(nameof(LogSpawnStatistics), 10f, 10f); // Выводим статистику каждые 10 секунд
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _currentSpawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();

        if (selectedEnemyPrefab)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

            /*// Обновляем счетчик спавнов
            if (_spawnCount.ContainsKey(selectedEnemyPrefab))
            {
                _spawnCount[selectedEnemyPrefab]++;
            }
            _totalSpawnCount++;*/
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        float randomValue = Random.Range(0f, 100f);
        float cumulativeProbability = 0f;

        foreach (var enemy in enemiesToSpawn)
        {
            cumulativeProbability += enemy.spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                return enemy.enemyPrefab;
            }
        }

        return enemiesToSpawn[0].enemyPrefab;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validPositionFound = false;

        while (!validPositionFound)
        {
            float xPos = Random.value > 0.5f
                ? Random.Range(_mainCamera.orthographicSize * _mainCamera.aspect + minSpawnOffset, _mainCamera.orthographicSize * _mainCamera.aspect + maxSpawnOffset)
                : Random.Range(-_mainCamera.orthographicSize * _mainCamera.aspect - maxSpawnOffset, -_mainCamera.orthographicSize * _mainCamera.aspect - minSpawnOffset);

            float yPos = Random.value > 0.5f
                ? Random.Range(_mainCamera.orthographicSize + minSpawnOffset, _mainCamera.orthographicSize + maxSpawnOffset)
                : Random.Range(-_mainCamera.orthographicSize - maxSpawnOffset, -_mainCamera.orthographicSize - minSpawnOffset);

            spawnPosition = new Vector2(xPos, yPos);

            if (!IsInPlayerView(spawnPosition))
            {
                validPositionFound = true;
            }
        }

        return spawnPosition;
    }

    private bool IsInPlayerView(Vector2 position)
    {
        Vector2 screenPoint = _mainCamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
    
    private void DecreaseSpawnInterval()
    {
        if (_currentSpawnInterval > minSpawnInterval)
        {
            _currentSpawnInterval -= spawnIntervalDecrease;
            if (_currentSpawnInterval < minSpawnInterval)
            {
                _currentSpawnInterval = minSpawnInterval;
            }
        }
    }

    /*private void LogSpawnStatistics()
    {
        Debug.Log("Current Spawn Statistics:");
        foreach (var enemy in enemiesToSpawn)
        {
            if (_spawnCount.ContainsKey(enemy.enemyPrefab))
            {
                float actualProbability = (_spawnCount[enemy.enemyPrefab] / (float)_totalSpawnCount) * 100f;
                Debug.Log($"Enemy: {enemy.enemyPrefab.name}, Target Probability: {enemy.spawnProbability}%, Actual Probability: {actualProbability:F2}%");
            }
        }
        Debug.Log($"Current Spawn Interval: {_currentSpawnInterval:F2} seconds");
    }*/
}
