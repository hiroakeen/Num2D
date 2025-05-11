using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 選択されたピースの合計をチェックして破壊判定
/// </summary>
public class SelectionEvaluator : MonoBehaviour
{
    [SerializeField] private TargetNumberProvider targetProvider;
    [SerializeField] private BoardManager boardManager;

    public void EvaluateSelection(List<Piece> selectedPieces)
    {
        if (selectedPieces == null || selectedPieces.Count == 0) return;

        int total = 0;
        foreach (var piece in selectedPieces)
        {
            total += piece.Number;
        }

        Debug.Log($"Evaluating: {total} vs {targetProvider.TargetNumber}");

        if (total == targetProvider.TargetNumber)
        {
            foreach (var piece in selectedPieces)
            {
                boardManager.RemovePiece(piece);
                piece.AnimateDestroy();
            }

            boardManager.DropPieces();
            boardManager.RefillBoard(); 
            targetProvider.GenerateNewTarget();

        }
        else
        {
            Debug.Log("不一致: ピースは消えません");
        }
    }

}
