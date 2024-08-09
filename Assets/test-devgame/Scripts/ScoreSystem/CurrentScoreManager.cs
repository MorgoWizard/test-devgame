using System;
using UnityEngine;

public class CurrentScoreManager : MonoBehaviour
{
    public int CurrentScore { get; private set; }

    public static event Action<int> OnCurrentScoreChanged;

    private void OnEnable()
    {
        EnemyController.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(int enemyRewardScore)
    {
        CurrentScore += enemyRewardScore;
        OnCurrentScoreChanged?.Invoke(CurrentScore);
    }
}
