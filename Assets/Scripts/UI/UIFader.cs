using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// �t�F�[�h�A�E�g��Ɏw�肵���V�[���֑J��
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
