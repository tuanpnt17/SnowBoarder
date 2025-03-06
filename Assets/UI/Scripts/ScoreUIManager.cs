using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScoreUIManager : MonoBehaviour
{
    private UnityEngine.UIElements.Label scoreLabel;
    private UnityEngine.UIElements.VisualElement healthContainer;
    private static ScoreUIManager instance;
    private string[] gameScenes = { "Level_01", "Level_02", "Level_03" };
    private Texture2D snowflakeTexture;

    public static ScoreUIManager Instance
    {
        get { return instance; }
    }

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
        snowflakeTexture = Resources.Load<Texture2D>("snowflake");
        if (snowflakeTexture == null)
        {
            UnityEngine.Debug.LogError("Không tìm thấy ảnh snowflake trong Resources!");
        }
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
        healthContainer = root.Q<VisualElement>("HealthContainer");
        if (scoreLabel == null)
        {
            UnityEngine.Debug.LogError("Không tìm th?y health");
            return;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_01")
        {
            //ResetScore();
            //ResetHealth();
        }
        //if (System.Array.Exists(gameScenes, level => level == scene.name))
        //{
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
        bool isGameScene = System.Array.Exists(gameScenes, level => level == scene.name);
        gameObject.SetActive(isGameScene);
    }

    public void UpdateScore(int newScore)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "SCORE: " + newScore;
        }
    }

    /// <summary>
    /// Reset điểm khi bắt đầu game mới.
    /// </summary>
    public void ResetScore()
    {
        //score = 0;
        //UpdateScore(score);
    }

    /// <summary>
    /// Cập nhật số lượng tim (snowflake) trên UI.
    /// </summary>
    public void UpdateHealthUI(int currHealth)
    {
        if (healthContainer == null)
        {
            UnityEngine.Debug.LogError("HealthContainer không tồn tại!");
            return;
        }
        // Xóa toàn bộ icon cũ
        healthContainer.Clear();

        for (int i = 0; i < currHealth; i++)
        {
            VisualElement snowflake = new VisualElement();
            snowflake.style.width = 56;
            snowflake.style.height = 48;
            snowflake.style.marginRight = 10;
            snowflake.style.marginLeft = 10;
            snowflake.style.backgroundImage = new StyleBackground(snowflakeTexture);

            healthContainer.Add(snowflake);
        }
    }

    ///// <summary>
    ///// Reset số tim về mặc định.
    ///// </summary>
    //public void ResetHealth()
    //{
    //    CurrentHealth = maxHealth;
    //    UpdateHealthUI();
    //}

    ///// <summary>
    ///// Lấy số tim hiện tại.
    ///// </summary>
    //public int GetHealth()
    //{
    //    return CurrentHealth;
    //}
}
