using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics; 

public class ScoreUIManager : MonoBehaviour
{
    private UnityEngine.UIElements.Label scoreLabel; 
    private static ScoreUIManager instance;
    private int score = 0;
    private string[] gameScenes = { "Level_01", "Level_02", "Level_03" };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
       
        UpdateScore(100);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null)
        {
            UnityEngine.Debug.LogError("rootVisualElement null! Kiểm tra UI Document trong Scene");
            return;
        }
        scoreLabel = root.Q<UnityEngine.UIElements.Label>("scoreLabel");
        if (scoreLabel == null)
        {
            UnityEngine.Debug.LogError("Không tìm th?y scoreLabel");
            return;
        }
        UpdateScore(100);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    //    if (System.Array.Exists(gameScenes, level => level == scene.name))
    //    {
    //        gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
        gameObject.SetActive(true);
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score;
        }
    }
}
