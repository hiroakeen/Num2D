using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI�Q��")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void OnPauseButtonPressed()
    {
        SEManager.PlayClick(); //�{�^����
        isPaused = true;
        Time.timeScale = 0f; // �Q�[����~
        pauseMenuPanel.SetActive(true);

        int best = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"�n�C�X�R�A{best}��";
    }

    public void OnResumeButtonPressed()
    {
        SEManager.PlayClick(); //�{�^����
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    public void OnQuitButtonPressed()
    {
        SEManager.PlayClick(); //�{�^����
        Time.timeScale = 1f; // �O�̂��ߍĊJ
        SceneManager.LoadScene("TitleScene");
    }
}
