using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    
    private EntityHealth _entityHealth;
    private EnemyFollower _enemyFollower;

    public static event Action<int> OnEnemyDeath;

    [SerializeField] private int rewardScore;

    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _enemyFollower = GetComponent<EnemyFollower>();
        
        Initialize();
    }

    private void OnEnable()
    {
        _entityHealth.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _entityHealth.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        OnEnemyDeath?.Invoke(rewardScore);
    }

    private void Initialize()
    {
        _enemyFollower.SetSpeed(enemyData.MoveSpeed);
        _entityHealth.SetHealth(enemyData.MaxHealth);
        rewardScore = enemyData.RewardScore;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EntityHealth>().InstantKill();
        }
    }
}
