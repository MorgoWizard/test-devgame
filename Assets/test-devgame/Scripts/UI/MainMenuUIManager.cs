using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private Button startButton;

    [SerializeField] private string highscoreText;
    [SerializeField] private TextMeshProUGUI highscoreTextTMP;

    private void Start()
    {
        startButton.onClick.AddListener(() => SceneController.LoadScene(gameplaySceneName));

        highscoreTextTMP.text = $"{highscoreText}{HighscoreManager.Instance.HighScore}";
    }
}
