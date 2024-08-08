using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    
    private EntityHealth _entityHealth;
    private EnemyFollower _enemyFollower;

    [SerializeField] private int rewardScore;

    public void Initialize()
    {
        _enemyFollower.SetSpeed(enemyData.MoveSpeed);
        _entityHealth.SetHealth(enemyData.MaxHealth);
        rewardScore = enemyData.RewardScore;
    }
}
