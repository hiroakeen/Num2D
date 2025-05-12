using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// 数字ピース（物理落下＋接地検出）
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public class Piece : MonoBehaviour
{
    public int Number { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsSettled { get; private set; } = false;

    [Header("Visual")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] numberSprites;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int number)
    {
        Number = number;
        IsSettled = false;
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
        if (number >= 1 && number <= 9 && numberSprites.Length >= 9)
        {
            spriteRenderer.sprite = numberSprites[number - 1];
        }
    }

    public void AnimateDrop(Vector3 targetPos, float duration = 0.2f)
    {
        transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
    }

    public void AnimateDestroy(float duration = 0.2f)
    {
        transform.DOScale(Vector3.zero, duration)
                 .SetEase(Ease.InBack)
                 .OnComplete(() => Destroy(gameObject));
    }

    /// <summary>
    /// 地面や他ピースと接触したら「接地済み」に
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsSettled)
        {
            IsSettled = true;
        }
    }

    public float GetColliderRadius()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        return collider != null ? collider.radius : 0.5f; // fallback
    }

}
