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

        // 初期状態
        countdownImage.rectTransform.localScale = Vector3.zero;
        CanvasGroup cg = countdownImage.GetComponent<CanvasGroup>();
        if (cg == null) cg = countdownImage.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        // 演出：フェード＋スケールアップ（浮き出る）
        cg.DOFade(1f, 0.4f);
        countdownImage.rectTransform
            .DOScale(1f, 0.6f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(2f);

        // Go表示へ差し替え + びよーん
        countdownImage.sprite = goSprite;
        countdownImage.rectTransform.localScale = Vector3.one * 0.6f;

        countdownImage.rectTransform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(1f);

        countdownImage.gameObject.SetActive(false);
    }

}
