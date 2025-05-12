using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// フェードアウト後に指定したシーンへ遷移
    /// </summary>
    public void FadeOutAndLoadScene(string sceneName, float duration = 1f)
    {
        canvasGroup.DOFade(1f, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => SceneManager.LoadScene(sceneName));
    }
    public void FadeIn(float duration = 1f)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, duration).SetEase(Ease.OutQuad);
    }

}
