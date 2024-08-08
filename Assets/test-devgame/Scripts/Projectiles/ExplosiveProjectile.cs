using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosiveRange;

    [SerializeField] private LayerMask targetLayerMask;
    
    protected override void OnTargetReached()
    {
        Explode();
    }
    
    private void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosiveRange, targetLayerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out EntityHealth currentEntityHealth))
            {
                currentEntityHealth.TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }
}
