using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    [SerializeField] private Transform target; // Целевой Transform, к которому нужно двигаться
    [SerializeField] private float moveSpeed = 5f; // Скорость движения

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        if (!target) return;
        
        // Определяем направление к цели
        Vector2 direction = (target.position - transform.position).normalized;
            
        // Двигаем объект в этом направлении
        _rigidbody2D.velocity = direction * moveSpeed;
    }

    private void OnDrawGizmos()
    {
        if (!target) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }

    public void SetSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
}
