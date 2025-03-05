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

    public void HandleCrash()
    {
        _currentHealth--;
        if (_currentHealth > 0)
        {
            skipStart = true;
            // Reload the scene
            Invoke(nameof(ReloadScene), _loadDelay);
        }
        else
        {
            // Endgame
            Debug.Log("Endgame");
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

    private void ReloadScene()
    {
        SceneManager.LoadScene(1); // Reload the scene hoangxuan
    }

    private void ChangeScore(int scoreChange)
    {
        _currentScore += scoreChange;
        Debug.Log("Score: " + _currentScore);
        // Update UI
    }

    private void CreateFloatingScore(int score, Vector3 pos)
    {
        GameObject floatingPoints = Instantiate(_floatingPointsPrefab, pos, Quaternion.identity);
        TextMesh textMesh = floatingPoints.GetComponentInChildren<TextMesh>();
        textMesh.text = (score >= 0 ? "+" : "") + score;
        textMesh.color = (score >= 0) ? Color.green : Color.red;
        Destroy(floatingPoints, 1f);
    }
}
