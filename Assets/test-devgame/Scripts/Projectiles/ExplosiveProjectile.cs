using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosiveRange;
    
    protected override void OnTargetReached()
    {
        Explode();
    }

    // TODO: add explosive damage logic
    private void Explode()
    {
        
    }
}
