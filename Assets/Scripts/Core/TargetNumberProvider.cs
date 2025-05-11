using UnityEngine;

/// <summary>
/// スコアに応じてターゲット値を変化させる
/// </summary>
public class TargetNumberProvider : MonoBehaviour
{
    public int TargetNumber { get; private set; }

    [SerializeField] private GameUIController uiController;

    /// <summary>
    /// スコアに応じたターゲット値をランダムに生成
    /// </summary>
    public void GenerateNewTarget(int score)
    {
        // スコアが上がるほどターゲットが上がる（最大24）
        int min = Mathf.Clamp(6 + score, 6, 20);
        int max = Mathf.Clamp(min + 4, 10, 24);

        TargetNumber = Random.Range(min, max + 1);
        Debug.Log($"🎯 新しいターゲット: {TargetNumber}");

        // UI更新（あれば）
        uiController?.UpdateTarget(TargetNumber);
    }
}
