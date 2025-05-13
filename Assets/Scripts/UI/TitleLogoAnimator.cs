using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleLogoAnimator : MonoBehaviour
{
    [Header("���S")]
    [SerializeField] private RectTransform logoTransform;
    [SerializeField] private CanvasGroup logoCanvasGroup;

    [Header("�X�^�[�g�{�^��")]
    [SerializeField] private RectTransform startButtonTransform;
    [SerializeField] private CanvasGroup startButtonCanvasGroup;

    private void Start()
    {
        AnimateTitle();
    }

    private void AnimateTitle()
    {
        // �������
        logoCanvasGroup.alpha = 0f;
        logoTransform.localScale = Vector3.zero;

        startButtonCanvasGroup.alpha = 0f;
        startButtonTransform.anchoredPosition += new Vector2(0, -100f); // ���ɉB��

        Sequence seq = DOTween.Sequence();

        // ���S���o
        seq.Append(logoCanvasGroup.DOFade(1f, 0.6f))
           .Join(logoTransform.DOScale(1.1f, 0.6f).SetEase(Ease.OutBack))
           .Append(logoTransform.DOScale(1f, 0.2f).SetEase(Ease.OutSine));

        // �����҂��ă{�^���o��
        seq.AppendInterval(0.3f);

        seq.Append(startButtonCanvasGroup.DOFade(1f, 0.4f))
           .Join(startButtonTransform.DOAnchorPosY(startButtonTransform.anchoredPosition.y + 100f, 0.4f)
           .SetEase(Ease.OutCubic));
    }
}
