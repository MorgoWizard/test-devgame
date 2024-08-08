using UnityEngine;

public class UnlimitedRangeBullet : Projectile
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
