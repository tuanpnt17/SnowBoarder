﻿using System.IO;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int bestScore;
}

public class BestScoreManager : MonoBehaviour
{
    private static string filePath; // Đường dẫn file JSON
    public static BestScoreManager Instance { get; private set; }
    public int currentScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        filePath = Application.persistentDataPath + "/bestscore.json";
        Debug.Log("✅ File JSON sẽ được lưu tại: " + filePath);
    }

    /// <summary>
    /// Lưu điểm cao nhất vào file JSON
    /// </summary>
    public void SaveBestScore(int score)
    {
        ScoreData data = new ScoreData();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ScoreData>(json);
        }

        if (score > data.bestScore)
        {
            data.bestScore = score;
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
        Debug.Log($"Best score: {data.bestScore}");
    }

    /// <summary>
    /// Tải điểm cao nhất từ file JSON
    /// </summary>
    public int LoadBestScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            return data.bestScore;
        }
        return 0;
    }

    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }
}
