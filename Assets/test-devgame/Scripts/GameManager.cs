using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EntityHealth characterHealth;
    [SerializeField] private CurrentScoreManager currentScoreManager;

    public static event Action OnGameOver; 

    private void OnEnable()
    {
        characterHealth.OnDeath += HandlePlayerDeath;
    }
    
    private void OnDisable()
    {
        characterHealth.OnDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
        if (HighscoreManager.Instance.HighScore < currentScoreManager.CurrentScore)
        {
            HighscoreManager.Instance.SetHighScore(currentScoreManager.CurrentScore);
        }
        
    }
}
