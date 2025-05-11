using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// なぞり選択されたピースを評価し、合計がターゲットと一致すれば処理する
/// スコア加算とターゲット更新にも対応
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

        if (sum == targetProvider.TargetNumber)
        {
            // 成功：ピース消去、スコア加算、ターゲット更新
            foreach (var piece in selectedPieces)
            {
                piece.AnimateDestroy();
            }

            GameManager.Instance?.AddScore(1);
            targetProvider.GenerateNewTarget(GameManager.Instance.Score);
        }
        else
        {
            Debug.Log("❌ 合計が一致しません");
        }

        uiController?.ClearCurrentSum();
    }
}
