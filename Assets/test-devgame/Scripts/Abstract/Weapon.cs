using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }
    [SerializeField] protected float fireRate;
    
    [SerializeField] protected Projectile projectilePrefab;

    protected float NextFireTime = 0f;

    public abstract void Attack(Vector2 targetPosition);

    protected bool CanFire()
    {
        return Time.time >= NextFireTime;
    }
}