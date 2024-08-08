using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float damage;
    
    protected virtual void Update()
    {
        Move();
        Debug.DrawRay(transform.position,transform.right);
    }

    private void Move()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
    protected abstract void OnCollisionEnter2D(Collision2D collision);
}