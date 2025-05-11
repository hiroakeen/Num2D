using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// �����s�[�X�F�����ځ{�A�j�����o�t��
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
    /// �������o�F���炩�Ɉړ�
    /// </summary>
    public void AnimateDrop(Vector3 targetPos, float duration = 0.2f)
    {
        transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// �������o�F�������Ȃ��ď�����
    /// </summary>
    public void AnimateDestroy(float duration = 0.2f)
    {
        transform.DOScale(Vector3.zero, duration)
                 .SetEase(Ease.InBack)
                 .OnComplete(() => Destroy(gameObject));
    }
}
