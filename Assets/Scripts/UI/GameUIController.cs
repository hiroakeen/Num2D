using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UIの表示管理（ターゲット数と現在の合計など）
/// </summary>
public class GameUIController : MonoBehaviour
{
    [Header("表示UI")]
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI currentSumText;
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("ゲーム終了演出（Image方式）")]
    [SerializeField] private Image finishImage;              // ← "FINISH" イラスト画像
    [SerializeField] private CanvasGroup finishCanvasGroup;  // ← 透過制御用

    /// <summary>
    /// タイムアップ時にFINISH画像をアニメ付きで表示
    /// </summary>
    public void ShowFinishImage()
    {
        if (finishImage == null || finishCanvasGroup == null)
        {
            Debug.LogWarning("Finish用のImageまたはCanvasGroupが設定されていません。");
            return;
        }

        finishCanvasGroup.alpha = 0f;
        finishCanvasGroup.gameObject.SetActive(true);

        finishCanvasGroup.DOFade(1f, 0.6f).SetEase(Ease.OutSine);
        finishImage.rectTransform.localScale = Vector3.zero;
        finishImage.rectTransform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
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
    /// 警告メッセージをアニメ表示
    /// </summary>
    public void ShowWarning(string message, float duration = 1.0f)
    {
        warningText.text = message;
        warningText.alpha = 0f;
        warningText.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(warningText.DOFade(1f, 0.3f))
           .AppendInterval(duration)
           .Append(warningText.DOFade(0f, 0.5f))
           .OnComplete(() => warningText.gameObject.SetActive(false));
    }
}
