using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private Image countdownImage;
    [SerializeField] private Sprite readySprite;
    [SerializeField] private Sprite goSprite;

    public IEnumerator PlayCountdown()
    {
        countdownImage.sprite = readySprite;
        countdownImage.gameObject.SetActive(true);

        // �������
        countdownImage.rectTransform.localScale = Vector3.zero;
        CanvasGroup cg = countdownImage.GetComponent<CanvasGroup>();
        if (cg == null) cg = countdownImage.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        // ���o�F�t�F�[�h�{�X�P�[���A�b�v�i�����o��j
        cg.DOFade(1f, 0.4f);
        countdownImage.rectTransform
            .DOScale(1f, 0.6f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(2f);

        // Go�\���֍����ւ� + �т�[��
        countdownImage.sprite = goSprite;
        countdownImage.rectTransform.localScale = Vector3.one * 0.6f;

        countdownImage.rectTransform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(1f);

        countdownImage.gameObject.SetActive(false);
    }

}
