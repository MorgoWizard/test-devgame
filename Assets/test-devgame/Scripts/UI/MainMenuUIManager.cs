using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() => SceneController.LoadScene(gameplaySceneName));
    }
}
