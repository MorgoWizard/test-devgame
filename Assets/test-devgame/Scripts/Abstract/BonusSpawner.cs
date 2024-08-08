using UnityEngine;
using System.Collections.Generic;

public abstract class BonusSpawner : MonoBehaviour
{
    [SerializeField] protected List<Bonus> bonusPool; // Пул бонусов
    [SerializeField] private float spawnInterval = 27f; // Интервал между спавнами

    private Camera _mainCamera;
    private float _spawnTimer;

    private void Awake()
    {
        _mainCamera = Camera.main; // Кешируем mainCamera
    }

    private void Start()
    {
        _spawnTimer = spawnInterval; // Устанавливаем таймер для первого спавна
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            SpawnBonus();
            _spawnTimer = spawnInterval; // Сброс таймера
        }
    }

    private void SpawnBonus()
    {
        Bonus bonusToSpawn = GetRandomBonus();
        if (bonusToSpawn)
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Instantiate(bonusToSpawn.gameObject, spawnPosition, Quaternion.identity);
        }
    }

    protected virtual Bonus GetRandomBonus()
    {
        if (bonusPool.Count == 0) return null;

        int randomIndex = Random.Range(0, bonusPool.Count);
        return bonusPool[randomIndex];
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validPositionFound = false;

        while (!validPositionFound)
        {
            float xPos = Random.Range(-_mainCamera.orthographicSize * _mainCamera.aspect - 10f, _mainCamera.orthographicSize * _mainCamera.aspect + 10f);
            float yPos = Random.Range(-_mainCamera.orthographicSize - 10f, _mainCamera.orthographicSize + 10f);

            spawnPosition = new Vector2(xPos, yPos);

            if (IsInCameraView(spawnPosition))
            {
                validPositionFound = true;
            }
        }

        return spawnPosition;
    }

    private bool IsInCameraView(Vector2 position)
    {
        Vector2 screenPoint = _mainCamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
}
