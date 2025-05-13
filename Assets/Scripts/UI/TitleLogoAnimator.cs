using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleLogoAnimator : MonoBehaviour
{
    [Header("ロゴ")]
    [SerializeField] private RectTransform logoTransform;
    [SerializeField] private CanvasGroup logoCanvasGroup;

    [Header("スタートボタン")]
    [SerializeField] private RectTransform startButtonTransform;
    [SerializeField] private CanvasGroup startButtonCanvasGroup;

    private void Start()
    {
        AnimateTitle();
    }

    private void AnimateTitle()
    {
        // 初期状態
        logoCanvasGroup.alpha = 0f;
        logoTransform.localScale = Vector3.zero;

        startButtonCanvasGroup.alpha = 0f;
        startButtonTransform.anchoredPosition += new Vector2(0, -100f); // 下に隠す

        Sequence seq = DOTween.Sequence();

        // ロゴ演出
        seq.Append(logoCanvasGroup.DOFade(1f, 0.6f))
           .Join(logoTransform.DOScale(1.1f, 0.6f).SetEase(Ease.OutBack))
           .Append(logoTransform.DOScale(1f, 0.2f).SetEase(Ease.OutSine));

        // 少し待ってボタン登場
        seq.AppendInterval(0.3f);

        seq.Append(startButtonCanvasGroup.DOFade(1f, 0.4f))
           .Join(startButtonTransform.DOAnchorPosY(startButtonTransform.anchoredPosition.y + 100f, 0.4f)
           .SetEase(Ease.OutCubic));
    }
}
