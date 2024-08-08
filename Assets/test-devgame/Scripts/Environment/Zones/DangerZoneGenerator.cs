using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Zone
{
    public GameObject zonePrefab;  // Префаб зоны
    public int numberOfZones;      // Количество зон данного типа
}

public struct PlacedZone
{
    public Vector2 zonePosition;
    public float zoneRadius;
}

public class DangerZoneGenerator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mapRenderer; // Компонент SpriteRenderer карты
    [SerializeField] private float minDistanceBetweenZones = 3f;
    [SerializeField] private List<Zone> zonesToGenerate; // Список зон для генерации

    private readonly List<PlacedZone> _generatedZones = new();
    private float _mapWidth;
    private float _mapHeight;
    private Vector2 _mapCenter;

    private void Start()
    {
        if (mapRenderer == null)
        {
            Debug.LogError("Необходимо назначить SpriteRenderer карты в инспекторе.");
            return;
        }

        // Определяем размеры карты на основе SpriteRenderer
        _mapWidth = mapRenderer.bounds.size.x;
        _mapHeight = mapRenderer.bounds.size.y;
        _mapCenter = mapRenderer.bounds.center;

        GenerateDangerZones();
    }

    private void GenerateDangerZones()
    {
        foreach (var zone in zonesToGenerate)
        {
            float zoneRadius = zone.zonePrefab.GetComponent<DangerZone>().Radius;
            int attempts = 0;
            int generatedCount = 0;

            while (generatedCount < zone.numberOfZones && attempts < zone.numberOfZones * 10)
            {
                Vector2 newZonePosition = GetRandomPositionWithinMap();
                
                if (IsPositionValid(newZonePosition, zoneRadius))
                {
                    PlacedZone placedZone = new PlacedZone
                    {
                        zonePosition = newZonePosition,
                        zoneRadius = zoneRadius
                    };
                    _generatedZones.Add(placedZone);
                    CreateDangerZone(zone.zonePrefab, newZonePosition);
                    generatedCount++;
                }

                attempts++;
            }

            if (generatedCount < zone.numberOfZones)
            {
                Debug.LogWarning($"Не удалось сгенерировать достаточное количество зон типа {zone.zonePrefab.name}. Попробуйте увеличить размер карты или уменьшить количество зон.");
            }
        }
    }

    private Vector2 GetRandomPositionWithinMap()
    {
        float halfMapWidth = _mapWidth / 2f;
        float halfMapHeight = _mapHeight / 2f;

        float x = Random.Range(_mapCenter.x - halfMapWidth + minDistanceBetweenZones, _mapCenter.x + halfMapWidth - minDistanceBetweenZones);
        float y = Random.Range(_mapCenter.y - halfMapHeight + minDistanceBetweenZones, _mapCenter.y + halfMapHeight - minDistanceBetweenZones);
        
        return new Vector2(x, y);
    }

    private bool IsPositionValid(Vector2 position, float zoneRadius)
    {
        // Проверка на расстояние от краев карты
        if (position.x < _mapCenter.x - _mapWidth / 2f + zoneRadius + minDistanceBetweenZones ||
            position.x > _mapCenter.x + _mapWidth / 2f - zoneRadius - minDistanceBetweenZones ||
            position.y < _mapCenter.y - _mapHeight / 2f + zoneRadius + minDistanceBetweenZones ||
            position.y > _mapCenter.y + _mapHeight / 2f - zoneRadius - minDistanceBetweenZones)
        {
            return false;
        }

        // Проверка на минимальное расстояние между зонами
        foreach (PlacedZone existingZone in _generatedZones)
        {
            if (Vector2.Distance(position, existingZone.zonePosition) < zoneRadius + minDistanceBetweenZones + existingZone.zoneRadius)
            {
                return false;
            }
        }

        return true;
    }


    private void CreateDangerZone(GameObject zonePrefab, Vector2 position)
    {
        GameObject zone = Instantiate(zonePrefab, position, Quaternion.identity);
        zone.transform.parent = transform; // Закрепляем зону как дочерний объект
    }
}
