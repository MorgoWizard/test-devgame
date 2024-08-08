using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;

    private void Awake()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(GameObject character);
}
