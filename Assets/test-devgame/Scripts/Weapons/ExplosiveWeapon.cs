using UnityEngine;

public class ExplosiveWeapon : Weapon
{
    [SerializeField] private Transform firePoint;

    public override void Attack(Vector2 targetPosition)
    {
        if (!CanFire()) return;
        
        LaunchProjectile(targetPosition);
        NextFireTime = Time.time + 1f / fireRate;
    }

    private void LaunchProjectile(Vector2 targetPosition)
    {
        ExplosiveProjectile explosiveProjectile = Instantiate(projectilePrefab.gameObject, firePoint.position, firePoint.rotation).GetComponent<ExplosiveProjectile>();
        explosiveProjectile.InitializeDestroyViaTargetReached(targetPosition);
    }
}