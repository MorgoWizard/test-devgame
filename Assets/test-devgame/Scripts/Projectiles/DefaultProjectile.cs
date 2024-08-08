using UnityEngine;

public class DefaultProjectile : Projectile
{
    // TODO: add damage logic
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
