using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    public event Action OnDeath;
    public event Action<float> OnDamageTaken;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0)
        {
            return;
        }

        _currentHealth -= damage;
        OnDamageTaken?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void InstantKill()
    {
        if (_currentHealth <= 0)
        {
            return;
        }

        _currentHealth = 0;
        Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}