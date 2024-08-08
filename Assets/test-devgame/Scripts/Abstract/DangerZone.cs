using System;
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

    protected virtual void OnPlayerEnter(GameObject character)
    {
        
    }

    protected virtual void OnPlayerExit(GameObject character)
    {
        
    }

    protected virtual void OnPlayerStay(GameObject character)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerStay(other.gameObject);
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