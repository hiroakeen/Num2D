using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.XR;

/// <summary>
/// ピースの合計値チェック、消去、連鎖再評価を担当
/// </summary>
public class SelectionEvaluator : MonoBehaviour
{
    [SerializeField] private TargetNumberProvider targetProvider;
    [SerializeField] private GameUIController uiController;

    public void EvaluateSelection(List<Piece> selectedPieces)
    {
        if (selectedPieces == null || selectedPieces.Count == 0) return;

        int sum = 0;
        foreach (var piece in selectedPieces)
        {
            sum += piece.Number;
        }

        Debug.Log($"Evaluating: {sum} vs {targetProvider.TargetNumber}");

        if (sum == targetProvider.TargetNumber)
        {
            Debug.Log("✅ 一致 → ピース削除 & 再評価へ");

            foreach (var piece in selectedPieces)
            {
                piece.AnimateDestroy();
            }

            targetProvider.GenerateNewTarget();

            StartCoroutine(DelayedReevaluate());
        }
        else
        {
            Debug.Log("❌ 不一致 → 何も起きない");
        }

        uiController.UpdateTarget(targetProvider.TargetNumber);
    }

    IEnumerator DelayedReevaluate()
    {
        yield return new WaitForSeconds(0.4f); // アニメ完了待ち
        TryAutoChain(); // 連鎖再評価（単純版）
    }

    void TryAutoChain()
    {
        // 現在の接地済みピースから、なぞらずに自動検出 or スキップ
        Debug.Log("🔁 連鎖チェック（未実装：自由にカスタム可能）");
        // → 将来的に自動選択による連鎖などを導入してもOK
    }


}
