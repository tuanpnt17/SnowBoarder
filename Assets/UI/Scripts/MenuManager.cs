using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject instructionUI;
    public TextMeshProUGUI HighestScore;
    public TextMeshProUGUI CurrentScore;
    private BestScoreManager bestScoreManager;

    void Start()
    {
        bestScoreManager = FindAnyObjectByType<BestScoreManager>(); // Tự động tìm ScoreManager

        if (bestScoreManager == null)
        {
            return;
        }

        //Giả sử người chơi đạt 120 điểm
        //int currentScore = 120;
        //scoreManager.SaveBestScore(currentScore);

        //int bestScore = ScoreManager.Instance.LoadBestScore();

        if (SceneManager.GetActiveScene().name.Equals("EndGame"))
        {
            var currentScore = GameManager.Instance.GetCurrentScore();
            CurrentScore.text = currentScore.ToString();
            bestScoreManager.SaveBestScore(currentScore);
            GameManager.Instance.ResetAll();
        }
        int bestScore = bestScoreManager.LoadBestScore();
        HighestScore.text = bestScore.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnInstructionPress()
    {
        instructionUI.SetActive(true);
    }

    public void OnInstructionClose()
    {
        instructionUI.SetActive(false);
    }

    public void OnPlayPress()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void OnExitPress()
    {
        Application.Quit();
    }

    public void SetHighestScore(TextMeshProUGUI s)
    {
        HighestScore = s;
    }

    public void SetCurrentScore(int s)
    {
        CurrentScore.text = s.ToString();
    }
}
