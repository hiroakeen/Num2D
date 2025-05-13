using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 一筆描きで赤丸を描き、筆っぽい演出とともにフェードアウトして消える演出クラス
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class RedCircleDrawer : MonoBehaviour
{
    [Header("円の形状")]
    [SerializeField] private float radius = 0.7f;
    [SerializeField] private float drawDuration = 0.05f;
    [SerializeField] private int segments = 30;

    [Header("消えるタイミング")]
    [SerializeField] private float fadeDelay = 0.2f;         // 描いた後どれくらい表示するか
    [SerializeField] private float fadeDuration = 0.2f;      // フェードアウトにかける時間

    private LineRenderer line;

    private void Awake()
    {
        // LineRenderer 初期化
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.loop = false;
        line.useWorldSpace = true;

        // マテリアルと色設定（必要に応じてエディタ側で筆風マテリアルを指定）
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = new Color(1f, 0.2f, 0.2f, 1f);  // 赤色（透明度=1）
        line.endColor = new Color(1f, 0.2f, 0.2f, 1f);

        // 幅の変化（筆で書くような強弱）
        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0f, 0.24f);   // 書き始め：太め
        widthCurve.AddKey(0.4f, 0.18f); // 中盤：自然に細く
        widthCurve.AddKey(0.8f, 0.12f); // 終盤：さらに細く
        widthCurve.AddKey(1f, 0.08f);   // 書き終わり：スッと抜ける感じ
        line.widthCurve = widthCurve;
    }

    /// <summary>
    /// 指定位置に一筆描きの円を描く
    /// </summary>
    /// <param name="center">描画位置（ピースの中心）</param>
    public void DrawCircle(Vector3 center)
    {
        StartCoroutine(DrawCircleCoroutine(center));
    }

    /// <summary>
    /// 一筆描きアニメーション＋フェードアウト＆自動削除
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

        // 少し表示した後に消す
        yield return new WaitForSeconds(fadeDelay);

        // フェードアウトと縮小アニメーション
        line.material.DOFade(0f, fadeDuration).SetEase(Ease.OutQuad);
        transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.InBack);

        // 少し後に自動削除（フェード完了後）
        Destroy(gameObject, fadeDuration + 0.1f);
    }
}
