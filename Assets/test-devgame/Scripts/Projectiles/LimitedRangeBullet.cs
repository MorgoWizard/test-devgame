using UnityEngine;

public class LimitedRangeBullet : Projectile
{
    [SerializeField] private float maxRange;
    
    private void Start()
    {
        float flightTime = maxRange / speed;
        Destroy(gameObject, flightTime);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
