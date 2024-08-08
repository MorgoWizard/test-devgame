using UnityEngine;

public class CreateWallsAroundObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D wallPrefab;
    [SerializeField] private float wallThickness = 1f;

    private void Start()
    {
        CreateBoundaryWalls();
    }

    private void CreateBoundaryWalls()
    {
        // Получаем размеры спрайта
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("На объекте отсутствует SpriteRenderer.");
            return;
        }

        Bounds bounds = spriteRenderer.bounds;

        // Позиции для создания стен
        Vector2 leftWallPosition = new Vector2(bounds.min.x - wallThickness / 2, bounds.center.y);
        Vector2 rightWallPosition = new Vector2(bounds.max.x + wallThickness / 2, bounds.center.y);
        Vector2 topWallPosition = new Vector2(bounds.center.x, bounds.max.y + wallThickness / 2);
        Vector2 bottomWallPosition = new Vector2(bounds.center.x, bounds.min.y - wallThickness / 2);

        // Размеры стен
        Vector2 verticalWallSize = new Vector2(wallThickness, bounds.size.y + wallThickness * 2);
        Vector2 horizontalWallSize = new Vector2(bounds.size.x + wallThickness * 2, wallThickness);

        // Создаем стены
        CreateWall("LeftWall", leftWallPosition, verticalWallSize);
        CreateWall("RightWall", rightWallPosition, verticalWallSize);
        CreateWall("TopWall", topWallPosition, horizontalWallSize);
        CreateWall("BottomWall", bottomWallPosition, horizontalWallSize);
    }

    private void CreateWall(string wallName, Vector2 position, Vector2 size)
    {
        var wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.size = size;
        wall.transform.parent = transform;
        wall.name = wallName;
    }
}
