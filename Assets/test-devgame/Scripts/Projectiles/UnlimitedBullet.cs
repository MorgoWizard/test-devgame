using UnityEngine;

public class UnlimitedBullet : Projectile
{
    protected override void Move()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
