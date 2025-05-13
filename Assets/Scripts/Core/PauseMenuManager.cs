using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void OnPauseButtonPressed()
    {
        isPaused = true;
        Time.timeScale = 0f; // ゲーム停止
        pauseMenuPanel.SetActive(true);

        int best = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"ハイスコア{best}問";
    }

    public void OnResumeButtonPressed()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    public void OnQuitButtonPressed()
    {
        Time.timeScale = 1f; // 念のため再開
        SceneManager.LoadScene("TitleScene");
    }
}
