using UnityEngine;
using TMPro;

/// <summary>
/// ゲーム全体の制限時間・スコアを管理するシングルトン
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("時間制限設定")]
    [SerializeField] private float timeLimit = 60f; // ゲームの制限時間（秒）
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("スコア表示")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private float remainingTime;
    private int score;
    private bool isGameRunning = true;

    public int Score => score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        remainingTime = timeLimit;
        score = 0;
        UpdateScoreUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isGameRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isGameRunning = false;
            EndGame();
        }

        UpdateTimerUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"{score}問";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"{remainingTime:F0}";
    }

    private void EndGame()
    {
        Debug.Log("⏰ タイムアップ！ゲーム終了");
        // TODO: 結果画面の表示やリスタート処理などをここに追加
    }
}
