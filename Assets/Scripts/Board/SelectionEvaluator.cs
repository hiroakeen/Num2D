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
    [SerializeField] private GameObject redCirclePrefab;


    public void EvaluateSelection(List<Piece> selectedPieces)
    {
        if (selectedPieces == null || selectedPieces.Count == 0) return;

        int sum = 0;
        foreach (var piece in selectedPieces)
        {
            sum += piece.Number;
        }

        int target = targetProvider.TargetNumber;

        if (selectedPieces.Count < 3)
        {
            // 少なすぎる → 揺らす＋メッセージ
            foreach (var piece in selectedPieces)
            {
                piece.AnimateShake();
            }
            uiController?.ShowWarning("3つ いじょう\nなぞってください");
        }
        else if (sum != target)
        {
            // 合計が違う → 揺らすだけ
            foreach (var piece in selectedPieces)
            {
                piece.AnimateShake();
            }
        }
        else
        {
            // 正解
            foreach (var piece in selectedPieces)
            {
                piece.AnimateDestroy();

                // 赤丸演出
                var circleObj = Instantiate(redCirclePrefab);
                var drawer = circleObj.GetComponent<RedCircleDrawer>();
                drawer.DrawCircle(piece.transform.position);
            }

            GameManager.Instance?.AddScore(1);
            targetProvider.GenerateNewTarget(GameManager.Instance.Score);
        }

        uiController?.ClearCurrentSum();
    }

}
