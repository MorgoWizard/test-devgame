using UnityEngine;

public class SpreadWeapon : Weapon
{
    [SerializeField] private int projectileCount = 5; // Количество снарядов
    [SerializeField] private float coneAngle = 10f; // Угол сектора в градусах
    
    [SerializeField] private Transform firePoint;

    public override void Attack(Vector2 targetPosition)
    {
        if (CanFire())
        {
            Vector2 direction = firePoint.right; // Используем направление firePoint

            float startAngle = -coneAngle / 2;
            float angleIncrement = coneAngle / (projectileCount - 1);

            for (int i = 0; i < projectileCount; i++)
            {
                float currentAngle = startAngle + angleIncrement * i;
                Vector2 projectileDirection = RotateVector2(direction, currentAngle);
                FireProjectile(firePoint.position, projectileDirection);
            }

            NextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void FireProjectile(Vector2 firePointPosition, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        GameObject projectile = Instantiate(projectilePrefab.gameObject, firePointPosition, rotation);
    }

    private Vector2 RotateVector2(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        
        float tx = v.x;
        float ty = v.y;
        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}