using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の制限時間とスコアを管理するシングルトン。カウントダウン後にゲーム開始。
/// タイムアップ時には演出を入れてFinishSceneに遷移する。
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("時間制限設定")]
    [SerializeField] private float timeLimit = 60f;               // ゲーム全体の制限時間（秒）
    [SerializeField] private TextMeshProUGUI timerText;           // 残り時間表示

    [Header("スコア表示")]
    [SerializeField] private TextMeshProUGUI scoreText;           // スコア表示

    [Header("参照オブジェクト")]
    [SerializeField] private CountdownUI countdownUI;             // Ready → Go 演出
    [SerializeField] private PieceSpawner pieceSpawner;           // ピース生成制御
    [SerializeField] private GameUIController uiController;       // UI全般制御（タイマーやゲームオーバー表示）

    [Header("フェードアウト")]
    [SerializeField] private UIFader uiFader;

    private float remainingTime;
    private int score;
    private bool isGameRunning = false;

    public int Score => score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private IEnumerator Start()
    {
        // ゲーム開始前のカウントダウン演出
        yield return StartCoroutine(countdownUI.PlayCountdown());

        // カウントダウン後、ピース連続スポーンモードを終了
        pieceSpawner.SetEarlyRush(false);

        // ゲーム本編開始
        remainingTime = timeLimit;
        score = 0;
        isGameRunning = true;

        UpdateScoreUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isGameRunning) return;

        // 時間経過処理
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            GameOver(); // タイムアップ処理
        }

        UpdateTimerUI();
    }

    /// <summary>
    /// スコア加算処理（正解したら呼ぶ）
    /// </summary>
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    /// <summary>
    /// スコア表示更新（UI）
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"{score}問";
    }

    /// <summary>
    /// 残り時間表示更新（UI）
    /// </summary>
    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"{remainingTime:F0}";
    }

    /// <summary>
    /// ゲーム終了処理（タイムアップ時に呼ばれる）
    /// </summary>
    public void GameOver()
    {
        if (!isGameRunning) return;

        isGameRunning = false;

        // 操作や生成を止める（演出的に停止に見せる）
        pieceSpawner.enabled = false;

        // タイムアップ表示
        uiController?.ShowFinishImage();

        // 2秒後に結果シーンへ遷移
        StartCoroutine(GoToFinishSceneAfterDelay());
    }

    /// <summary>
    /// 2秒の待機後にFinishSceneへ遷移
    /// </summary>
    private IEnumerator GoToFinishSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();

        uiFader?.FadeOutAndLoadScene("FinishScene", 1f);  // ← フェードアウトで遷移
    }

}
