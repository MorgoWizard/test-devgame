using UnityEngine;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Transform firePoint;

    public override void Attack(Vector2 targetPosition)
    {
        if (!CanFire()) return;
        
        FireBullet();
        NextFireTime = Time.time + 1f / fireRate;
    }

    private void FireBullet()
    {
        Instantiate(projectilePrefab.gameObject, firePoint.position, firePoint.rotation);
    }
}