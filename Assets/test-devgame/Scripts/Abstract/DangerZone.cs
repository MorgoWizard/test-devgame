using UnityEngine;

public abstract class DangerZone : MonoBehaviour
{
    [field: SerializeField] public float Radius { get; private set; }

    protected virtual void Start()
    {
        SetupZone();
    }

    private void SetupZone()
    {
        transform.localScale = new Vector3(Radius * 2, Radius * 2, 1);
    }
    
    protected abstract void OnPlayerEnter(GameObject player);

    protected abstract void OnPlayerExit(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerExit(collision.gameObject);
        }
    }
}