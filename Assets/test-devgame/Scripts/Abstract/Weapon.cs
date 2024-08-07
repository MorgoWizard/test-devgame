using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;

    protected float NextFireTime;

    public abstract void Attack(Vector2 targetPosition);

    protected bool CanFire()
    {
        return Time.time >= NextFireTime;
    }
}