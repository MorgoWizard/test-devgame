using UnityEngine;

public class DefaultProjectile : Projectile
{
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityHealth currentEntity))
        {
            currentEntity.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
