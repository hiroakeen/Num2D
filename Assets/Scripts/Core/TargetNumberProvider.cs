using UnityEngine;

/// <summary>
/// 現在のターゲット合計値を管理する
/// </summary>
public class TargetNumberProvider : MonoBehaviour
{
    public int TargetNumber { get; private set; }

    void Start()
    {
        GenerateNewTarget();
    }

    public void GenerateNewTarget()
    {
        TargetNumber = Random.Range(10, 21);
        Debug.Log($"🎯 新しいターゲット: {TargetNumber}");
    }
}
