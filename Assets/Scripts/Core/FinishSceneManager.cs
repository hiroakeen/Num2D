using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class FinishSceneManager : MonoBehaviour
{
    [Header("UI要素")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private UIFader fader;

    [Header("ボタン")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    [Header("カウントアップ設定")]
    [SerializeField] private float countDelay = 0.05f; // 数字の増加間隔

    private int finalScore;
    private int highScore;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject newRecordBanner; // "New Record!" 表示用（ImageやText）



    void Start()
    {
        // UI初期化
        retryButton.gameObject.SetActive(false);
        titleButton.gameObject.SetActive(false);
        scoreText.text = "";
        highScoreText.text = "";
        newRecordBanner.SetActive(false);

        // フェードイン
        fader.FadeIn(1f);

        // スコア取得
        finalScore = PlayerPrefs.GetInt("LastScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
      

        // ハイスコア更新チェック
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            newRecordBanner.SetActive(true); // ← New Record表示！
        }

        // 表示演出開始
        StartCoroutine(CountUpScore());
        // スコア登録
        ScoreRanking.RegisterScore(finalScore);
    }


    private IEnumerator CountUpScore()
    {
        int current = 0;
        yield return new WaitForSeconds(1f);

        while (current <= finalScore)
        {
            scoreText.text = $"正解数：{current} 問";
            current++;
            yield return new WaitForSeconds(countDelay);
        }

        // ハイスコア表示
        highScoreText.text = $"最高記録：{highScore} 問";

        yield return new WaitForSeconds(0.5f);

        retryButton.gameObject.SetActive(true);
        titleButton.gameObject.SetActive(true);
        retryButton.transform.DOScale(1f, 0.4f).From(0f).SetEase(Ease.OutBack);
        titleButton.transform.DOScale(1f, 0.4f).From(0f).SetEase(Ease.OutBack);
    }


    public void OnRetry()
    {
        fader.FadeOutAndLoadScene("MainScene", 1f);
    }

    public void OnTitle()
    {
        fader.FadeOutAndLoadScene("TitleScene", 1f);
    }
}
