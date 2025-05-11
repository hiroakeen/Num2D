using UnityEngine;

/// <summary>
/// ������[���s���s�[�X�X�|�i�[�B�ڒn�ς݃s�[�X�����Ď����A�K�v�Ȃ�ォ��s�[�X�𗎂Ƃ��B
/// Spawn�ʒu��Y���� Transform ����擾�\�B
/// </summary>
public class PieceSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private float spawnXRange = 2.5f;
    [SerializeField] private Transform spawnHeightPoint; // Y�ʒu�w��I�u�W�F�N�g

    [Header("��[����")]
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
