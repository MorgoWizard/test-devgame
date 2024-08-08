using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    
    private bool _isInvulnerable = false;

    public event Action OnDeath;
    public event Action<float> OnDamageTaken;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(_isInvulnerable) return;
        
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
        TakeDamage(_currentHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void ResetHealth()
    {
        _currentHealth = maxHealth;
    }

    public void SetHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        ResetHealth();
    }

    public void SetInvulnerable(bool status)
    {
        _isInvulnerable = status;
    }
}