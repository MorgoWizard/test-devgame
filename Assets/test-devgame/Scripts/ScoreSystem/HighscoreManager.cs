using UnityEngine;
using System.IO;

public class HighscoreManager : Singleton<HighscoreManager>
{
    public int HighScore { get; private set; }

    private string _filePath;
    private const string FileName = "highscore.dat";

    protected override void Awake()
    {
        base.Awake();
        
        #if UNITY_EDITOR
        string recordDirectory = Path.Combine(Application.dataPath, "test-devgame/Record");
        #else
        string recordDirectory = Path.Combine(Application.persistentDataPath, "Record");
        #endif
        
        if (!Directory.Exists(recordDirectory))
        {
            Directory.CreateDirectory(recordDirectory);
        }
        
        _filePath = Path.Combine(recordDirectory, FileName);

        LoadHighScore();
    }

    public void SetHighScore(int newScore)
    {
        if (newScore <= HighScore) return;
        
        HighScore = newScore;
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        string encryptedScore = EncryptionUtility.Encrypt(HighScore.ToString());
        File.WriteAllText(_filePath, encryptedScore);
    }

    private void LoadHighScore()
    {
        if (File.Exists(_filePath))
        {
            string encryptedScore = File.ReadAllText(_filePath);
            string decryptedScore = EncryptionUtility.Decrypt(encryptedScore);
            HighScore = int.TryParse(decryptedScore, out var loadedHighscore) ? loadedHighscore : 0;
        }
        else
        {
            HighScore = 0;
        }
    }
}