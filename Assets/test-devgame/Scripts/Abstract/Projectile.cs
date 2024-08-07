using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float damage;
    // [SerializeField] private float maxRange;

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();
    protected abstract void OnCollisionEnter2D(Collision2D collision);
}