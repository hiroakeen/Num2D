using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// UIの表示管理（ターゲット数と現在の合計）
/// </summary>
public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI currentSumText;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;

    /// <summary>
    /// タイムアップなどの表示をアニメーション付きで表示
    /// </summary>
    public void ShowGameOverText(string message)
    {
        gameOverText.text = message;
        gameOverCanvasGroup.alpha = 0f;
        gameOverCanvasGroup.gameObject.SetActive(true);

        // フェードイン + スケールポップ演出（同時）
        gameOverCanvasGroup.DOFade(1f, 0.6f).SetEase(Ease.OutSine);
        gameOverText.rectTransform.localScale = Vector3.zero;
        gameOverText.rectTransform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
    }

    public void UpdateTarget(int target)
    {
        targetText.text = $"{target}";
    }

    public void UpdateCurrentSum(int sum)
    {
        currentSumText.text = $"{sum}";
    }

    public void ClearCurrentSum()
    {
        currentSumText.text = "";
    }

    /// <summary>
    /// ワーニングメッセージをフェード表示
    /// </summary>
    public void ShowWarning(string message, float duration = 1.0f)
    {
        warningText.text = message;
        warningText.alpha = 0f;
        warningText.gameObject.SetActive(true);

        // フェードイン → 一定時間 → フェードアウト
        Sequence seq = DOTween.Sequence();
        seq.Append(warningText.DOFade(1f, 0.3f))
           .AppendInterval(duration)
           .Append(warningText.DOFade(0f, 0.5f))
           .OnComplete(() => warningText.gameObject.SetActive(false));
    }

  
    

}
