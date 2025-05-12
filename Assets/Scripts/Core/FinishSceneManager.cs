using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FinishSceneManager : MonoBehaviour
{
    [SerializeField] private UIFader fader;

    [Header("ボタン")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button titleButton;

    [Header("カウントアップ設定")]
    [SerializeField] private float countDelay = 0.05f; // 数字の増加間隔

    [Header("スコア表示")]
    private int finalScore;
    private int highScore;
    [SerializeField] private GameObject newRecordBanner; // "New Record!" 表示用
    [SerializeField] private TextMeshProUGUI rankingText;
    [SerializeField] private TextMeshProUGUI[] rankTexts;
    [SerializeField] private TextMeshProUGUI scoreText;       // 通常スコア表示
    [SerializeField] private TextMeshProUGUI highScoreText;   // ランキング入り用表示
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

        // スコア登録
        ScoreRanking.RegisterScore(finalScore);

        // ランキング取得
        var scores = ScoreRanking.LoadScores();
        bool isRanked = scores.Contains(finalScore);

        // スコア表示の切り替え
        if (isRanked)
        {
            highScoreText.gameObject.SetActive(true);
            highScoreText.text = $"ランキング入り！ {finalScore} 問";
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = $"正解数：{finalScore} 問";
            highScoreText.gameObject.SetActive(false);
        }

        // 表示演出開始
        StartCoroutine(CountUpScore());

        // ランキング表示
        ShowRanking(scores);
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

    void ShowRanking(List<int> scores)
    {
        for (int i = 0; i < rankTexts.Length; i++)
        {
            if (i == 0)
            {
                rankTexts[i].color = Color.yellow;
                rankTexts[i].fontSize = 42;
            }

            if (i < scores.Count)
                rankTexts[i].text = $"{i + 1}. {scores[i]} 問";
            else
                rankTexts[i].text = $"{i + 1}. ---";
        }
    }
}
