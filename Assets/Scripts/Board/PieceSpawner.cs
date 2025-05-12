using UnityEngine;

/// <summary>
/// 自動補充を行うピーススポナー。接地済みピース数を監視し、必要なら上からピースを落とす。
/// Spawn位置のY軸は Transform から取得可能。
/// 特定の数字（1〜3）の出現率を高めるため、重み付きランダムを使用。
/// </summary>
public class PieceSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private float spawnXRange = 2.0f;
    [SerializeField] private Transform spawnHeightPoint; // Y位置指定オブジェクト（空中の基準点）

    [Header("補充制御")]
    [SerializeField] private int maxSettledCount = 25;

    private float timer = 0f;
    private bool isEarlyRush = true;

    // --- 出現数値の重み付きランダムテーブル ---
    // 1〜3が他よりも多く含まれることで出現率が上昇
    private readonly int[] weightedNumbers = new int[]
    {
        1,1, 2,2, 3,3, 4, 5, 6, 7, 8, 9
    };

    void Update()
    {
        int settledCount = CountSettledPieces();

        // ラッシュ中は爆速、それ以外は通常間隔でスポーン
        float currentSpawnInterval = isEarlyRush ? 0.05f : spawnInterval;

        if (settledCount < maxSettledCount)
        {
            timer += Time.deltaTime;

            if (timer >= currentSpawnInterval)
            {
                SpawnPiece();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    /// <summary>
    /// ランダム位置にピースをスポーンし、ランダムな数字を設定
    /// </summary>
    void SpawnPiece()
    {
        float spawnY = spawnHeightPoint != null ? spawnHeightPoint.position.y : 5f;
        float spawnX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        GameObject obj = Instantiate(piecePrefab, spawnPos, Quaternion.identity);

        Piece piece = obj.GetComponent<Piece>();
        piece.Init(GetWeightedRandomNumber());
    }

    /// <summary>
    /// 現在のシーン上にある接地済みピース数をカウント
    /// </summary>
    int CountSettledPieces()
    {
        int count = 0;
        var pieces = FindObjectsByType<Piece>(FindObjectsSortMode.None);

        foreach (var piece in pieces)
        {
            if (piece.IsSettled) count++;
        }

        return count;
    }

    /// <summary>
    /// Early Rush（開始直後の連続スポーン）切替
    /// </summary>
    public void SetEarlyRush(bool value)
    {
        isEarlyRush = value;
    }

    /// <summary>
    /// 1〜3の数字が出やすくなるよう調整されたランダム数字を返す
    /// </summary>
    private int GetWeightedRandomNumber()
    {
        return weightedNumbers[Random.Range(0, weightedNumbers.Length)];
    }
}
