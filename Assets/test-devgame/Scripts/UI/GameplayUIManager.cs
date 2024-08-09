using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private Button[] mainMenuButtons;

    [SerializeField] private Button restartButton;

    [SerializeField] private string currentScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreTextTMP;
    
    [SerializeField] private string newHighscoreText;
    [SerializeField] private TextMeshProUGUI newHighscoreTextTMP;

    [SerializeField] private GameObject loseScreen;
    
    private void Start()
    {
        foreach (var button in mainMenuButtons)
        {
            button.onClick.AddListener(() => SceneController.LoadScene(mainMenuSceneName));
        }

        restartButton.onClick.AddListener(SceneController.RestartScene);
    }

    private void OnEnable()
    {
        CurrentScoreManager.OnCurrentScoreChanged += HandleCurrentScoreChange;
        HighscoreManager.OnNewHighscore += HandleNewHighscore;
        GameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        CurrentScoreManager.OnCurrentScoreChanged -= HandleCurrentScoreChange;
        HighscoreManager.OnNewHighscore -= HandleNewHighscore;
        GameManager.OnGameOver -= HandleGameOver;
    }

    private void HandleCurrentScoreChange(int currentScore)
    {
        currentScoreTextTMP.text = $"{currentScoreText}{currentScore}";
    }

    private void HandleNewHighscore(int newHighscore)
    {
        newHighscoreTextTMP.gameObject.SetActive(true);
        newHighscoreTextTMP.text = $"{newHighscoreText}{newHighscore}";
    }

    private void HandleGameOver()
    {
        loseScreen.SetActive(true);
    }
}
