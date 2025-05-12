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

    void Start()
    {
        // 初期設定：ボタン非表示、スコア非表示、フェード開始
        retryButton.gameObject.SetActive(false);
        titleButton.gameObject.SetActive(false);
        scoreText.text = "";
        fader.FadeIn(1f);

        // スコア読み込み
        finalScore = PlayerPrefs.GetInt("LastScore", 0);

        // カウントアップ演出開始
        StartCoroutine(CountUpScore());
    }

    private IEnumerator CountUpScore()
    {
        int current = 0;

        yield return new WaitForSeconds(1f); // 演出前の間

        while (current <= finalScore)
        {
            scoreText.text = $"正解数：{current} 問";
            current++;
            yield return new WaitForSeconds(countDelay);
        }

        // 少し間をおいてボタン表示
        yield return new WaitForSeconds(0.5f);

        retryButton.gameObject.SetActive(true);
        titleButton.gameObject.SetActive(true);

        // ボタンをフェード表示
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
