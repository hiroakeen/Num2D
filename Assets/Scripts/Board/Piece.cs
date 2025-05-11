using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// 数字ピース：見た目＋アニメ演出付き
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Piece : MonoBehaviour
{
    public int Number { get; private set; }
    public bool IsSelected { get; private set; }

    [Header("Visual")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private TextMeshProUGUI textDisplay;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int number)
    {
        Number = number;
        SetSelected(false);
        SetVisual(number);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        spriteRenderer.color = selected ? selectedColor : defaultColor;
    }

    public void SetVisual(int number)
    {
        if (textDisplay != null)
        {
            textDisplay.text = number.ToString();
        }
    }

    /// <summary>
    /// 落下演出：滑らかに移動
    /// </summary>
    public void AnimateDrop(Vector3 targetPos, float duration = 0.2f)
    {
        transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// 消去演出：小さくなって消える
    /// </summary>
    public void AnimateDestroy(float duration = 0.2f)
    {
        transform.DOScale(Vector3.zero, duration)
                 .SetEase(Ease.InBack)
                 .OnComplete(() => Destroy(gameObject));
    }
}
