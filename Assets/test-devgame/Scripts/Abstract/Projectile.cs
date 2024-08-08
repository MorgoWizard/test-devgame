using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float damage;
    
    [SerializeField] private float maxRange;
    
    private void Start()
    {
        if (maxRange > 0)
        {
            float flightTime = maxRange / speed;
            Destroy(gameObject, flightTime);
        }
    }

    private IEnumerator DestroyOnTargetReached(float flightTime)
    {
        yield return new WaitForSeconds(flightTime);
        OnTargetReached();
        Destroy(gameObject);
    }

    protected virtual void OnTargetReached()
    {
        
    }
    
    protected virtual void Update()
    {
        Move();
        Debug.DrawRay(transform.position,transform.right);
    }

    private void Move()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}