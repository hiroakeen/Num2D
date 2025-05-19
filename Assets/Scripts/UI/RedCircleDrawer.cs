using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 赤丸クラス
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class RedCircleDrawer : MonoBehaviour
{
    [Header("円の形状")]
    [SerializeField] private float radius = 0.7f;
    [SerializeField] private int segments = 30;

    [Header("消えるタイミング")]
    [SerializeField] private float fadeDelay = 0.2f;         
    [SerializeField] private float fadeDuration = 0.2f;      

    private LineRenderer line;

    private void Awake()
    {
        // LineRenderer 初期化
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.loop = false;
        line.useWorldSpace = true;

        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = new Color(1f, 0.2f, 0.2f, 1f);  // 赤
        line.endColor = new Color(1f, 0.2f, 0.2f, 1f);

        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0f, 0.24f);   // 書き始め
        widthCurve.AddKey(0.4f, 0.18f); // 中盤
        widthCurve.AddKey(0.8f, 0.12f); // 終盤
        widthCurve.AddKey(1f, 0.08f);   // 書き終わり
        line.widthCurve = widthCurve;
    }

    /// <summary>
    /// 指定位置に円を描く
    /// </summary>
    /// <param name="center">描画位置（ピースの中心）</param>
    public void DrawCircle(Vector3 center)
    {
        StartCoroutine(DrawCircleCoroutine(center));
    }

    /// <summary>
    /// Animation & Fadeout & Destroy
    /// </summary>
    private IEnumerator DrawCircleCoroutine(Vector3 center)
    {
        line.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float angle = -t * Mathf.PI * 2f;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Vector3 point = center + offset;
            line.SetPosition(i, point);
        }

        yield return new WaitForSeconds(0.1f);

        // 描き終わったら円を閉じる
        line.loop = true;

        // 表示した後は消す
        yield return new WaitForSeconds(fadeDelay);

        // フェードアウト　＋　縮小アニメーション
        line.material.DOFade(0f, fadeDuration).SetEase(Ease.OutQuad);
        transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.InBack);

        // フェード完了後に削除
        Destroy(gameObject, fadeDuration + 0.1f);
    }
}
