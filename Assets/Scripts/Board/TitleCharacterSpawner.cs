using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TitleCharacterSpawner : MonoBehaviour
{
    [Header("数字キャラプレハブ（1〜9）")]
    [SerializeField] private GameObject[] characterPrefabs;

    [Header("生成エリア（UI RectTransform）")]
    [SerializeField] private RectTransform spawnAreaRect;

    [Header("演出設定")]
    [SerializeField] private float fallDistance = 200f;
    [SerializeField] private float lifetime = 5f;

    [Header("スポーン設定")]
    [SerializeField] private float initialSpawnCount = 12;
    [SerializeField] private float spawnInterval = 1.5f; // 何秒ごとに1体追加

    void Start()
    {
        // 最初に複数体出現
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnCharacterWithEffects();
        }

        // 以降は定期スポーンを開始
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCharacterWithEffects();
        }
    }

    private void SpawnCharacterWithEffects()
    {
        Vector2 spawnPos = GetRandomPositionInRect();
        GameObject prefab = characterPrefabs[Random.Range(0, characterPrefabs.Length)];
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

        // 拡大・落下・フェード・削除の演出
        Sequence seq = DOTween.Sequence();

        Vector3 originalScale = obj.transform.localScale;
        obj.transform.localScale = Vector3.zero;
        seq.Append(obj.transform.DOScale(originalScale, 0.2f)); // ゼロ→通常サイズへ
        seq.Append(obj.transform.DOScale(originalScale * 1.2f, 0.2f).SetEase(Ease.OutBack)); // ちょい拡大
        seq.Append(obj.transform.DOScale(originalScale, 0.15f)); // 元に戻る

        seq.Join(obj.transform.DOMoveY(obj.transform.position.y - fallDistance, lifetime).SetEase(Ease.InQuad));

        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null) cg = obj.AddComponent<CanvasGroup>();
        cg.alpha = 1f;
        seq.Join(cg.DOFade(0f, 0.5f).SetDelay(lifetime - 0.5f));

        Destroy(obj, lifetime + 0.1f);
        ApplyRandomMotion(obj.transform);
    }

    private Vector2 GetRandomPositionInRect()
    {
        Vector2 size = spawnAreaRect.rect.size;
        float x = Random.Range(-size.x / 2f, size.x / 2f);
        float y = Random.Range(-size.y / 2f, size.y / 2f);
        Vector2 localPoint = new Vector2(x, y);
        return spawnAreaRect.TransformPoint(localPoint);
    }

    private void ApplyRandomMotion(Transform target)
    {
        int motionType = Random.Range(0, 10); // バリエーション増加！

        switch (motionType)
        {
            case 0: // ゆったり浮遊
                target.DOMoveY(target.position.y + 30f, 1.2f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.InOutSine);
                break;

            case 1: // 回転ループ
                target.DORotate(new Vector3(0, 0, 360f), 4f, RotateMode.FastBeyond360)
                      .SetLoops(-1)
                      .SetEase(Ease.Linear);
                break;

            case 2: // 左右に揺れる
                target.DOMoveX(target.position.x + 20f, 0.8f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.InOutSine);
                break;

            case 3: // ポップ（拡大縮小）
                target.DOScale(1.2f, 0.5f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.OutElastic);
                break;

            case 4: // ゆらゆら＋回転
                Sequence floatSpin = DOTween.Sequence();
                floatSpin.Append(target.DOMoveY(target.position.y + 25f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
                floatSpin.Join(target.DORotate(new Vector3(0, 0, 15f), 1.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad));
                break;

            case 5: // 落下（ゆっくり）
                target.DOMoveY(target.position.y - 250f, 6f).SetEase(Ease.InQuad);
                break;

            case 6: // 軽くジャンプ
                target.DOMoveY(target.position.y + 15f, 0.4f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.OutQuad);
                break;

            case 7: // 小さくバウンド
                target.DOScale(0.8f, 0.3f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.OutBounce);
                break;

            case 8: // 左右ぶるぶる
                target.DOMoveX(target.position.x + 10f, 0.1f)
                      .SetLoops(-1, LoopType.Yoyo)
                      .SetEase(Ease.InOutSine);
                break;

            case 9: // 全体ゆっくり縮小して消える（静止アニメ）
                target.DOScale(0f, 3f).SetEase(Ease.InBack);
                break;
        }
    }

}
