using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool skipStart = false;

    [SerializeField]
    private int _maxHealth = 3;

    [SerializeField]
    private float _loadDelay = 0.5f;

    [SerializeField]
    private int _coinScore = 10;

    [SerializeField]
    private int _rotationScore = 50;

    [SerializeField]
    private GameObject _floatingPointsPrefab;

    private int _currentScore = 0;
    private int _currentHealth = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        _currentHealth = _maxHealth;
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public void ResetAll()
    {
        _currentScore = 0;
        _currentHealth = _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void HandleCrash()
    {
        _currentHealth--;
        ScoreUIManager.Instance.UpdateHealthUI(_currentHealth);
        if (_currentHealth > 0)
        {
            Invoke(nameof(ReloadCurrentScene), _loadDelay);
        }
        else
        {
            // Endgame
            Invoke(nameof(Endgame), _loadDelay);
        }
    }

    public void CollectCoin(Vector3 pos)
    {
        ChangeScore(_coinScore);
        CreateFloatingScore(_coinScore, pos);
    }

    public void CompleteRotation(Vector3 pos)
    {
        ChangeScore(_rotationScore);
        CreateFloatingScore(_rotationScore, pos);
    }

    private void ReloadCurrentScene()
    {
        skipStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Endgame()
    {
        SceneManager.LoadScene("EndGame");
    }

    private void ChangeScore(int scoreChange)
    {
        _currentScore += scoreChange;
        Debug.Log("Score: " + _currentScore);
        // Update UI
        ScoreUIManager.Instance.UpdateScore(_currentScore);
    }

    private void CreateFloatingScore(int score, Vector3 pos)
    {
        GameObject floatingPoints = Instantiate(_floatingPointsPrefab, pos, Quaternion.identity);
        TextMesh textMesh = floatingPoints.GetComponentInChildren<TextMesh>();
        textMesh.text = (score >= 0 ? "+" : "") + score;
        //textMesh.color = (score >= 0) ? Color.magenta : Color.red;
        Destroy(floatingPoints, 1f);
    }
}
