using UnityEngine;

/// <summary>
/// 自動補充を行うピーススポナー。接地済みピース数を監視し、必要なら上からピースを落とす。
/// Spawn位置のY軸は Transform から取得可能。
/// </summary>
public class PieceSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private float spawnXRange = 2.5f;
    [SerializeField] private Transform spawnHeightPoint; // Y位置指定オブジェクト

    [Header("補充制御")]
    [SerializeField] private int maxSettledCount = 25;

    private float timer = 0f;

    void Update()
    {
        int settledCount = CountSettledPieces();

        if (settledCount < maxSettledCount)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
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

    void SpawnPiece()
    {
        float spawnY = spawnHeightPoint != null ? spawnHeightPoint.position.y : 5f;
        float spawnX = Random.Range(-spawnXRange, spawnXRange);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        GameObject obj = Instantiate(piecePrefab, spawnPos, Quaternion.identity);

        Piece piece = obj.GetComponent<Piece>();
        piece.Init(Random.Range(1, 10));
    }

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
}
