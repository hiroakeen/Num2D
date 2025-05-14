using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// なぞりによるピース選択とLineDrawerによる線表示
/// </summary>
public class PieceInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private SelectionEvaluator evaluator;
    [SerializeField] private GameUIController uiController;
    [SerializeField] private LineDrawer lineDrawer;

    private List<Piece> selectedPieces = new List<Piece>();
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedPieces.Clear();
            isDragging = true;
            TrySelectPieceAtMouse();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            TrySelectPieceAtMouse();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            evaluator.EvaluateSelection(selectedPieces);
            ClearSelection();
        }

        // 線をリアルタイムに更新
        lineDrawer?.UpdateLine(selectedPieces);
    }

    void TrySelectPieceAtMouse()
    {
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if (hit != null)
        {
            Piece piece = hit.GetComponent<Piece>();
            if (piece != null && piece.IsSettled && !selectedPieces.Contains(piece))
            {
                if (selectedPieces.Count == 0 || IsAdjacent(selectedPieces[selectedPieces.Count - 1], piece))
                {
                    selectedPieces.Add(piece);
                    piece.SetSelected(true);
                    piece.AnimateSelect();
                    uiController?.UpdateCurrentSum(GetCurrentSum());
                }
            }
        }
    }

    bool IsAdjacent(Piece a, Piece b)
    {
        float aSize = a.GetColliderRadius() * a.transform.localScale.x;
        float bSize = b.GetColliderRadius() * b.transform.localScale.x;
        float threshold = (aSize + bSize) * 1.2f;
        return Vector2.Distance(a.transform.position, b.transform.position) <= threshold;
    }

    void ClearSelection()
    {
        foreach (var piece in selectedPieces)
        {
            piece.SetSelected(false);
        }
        selectedPieces.Clear();
        uiController?.ClearCurrentSum();
        lineDrawer?.ClearLine(); // 線も消す
    }

    int GetCurrentSum()
    {
        int sum = 0;
        foreach (var piece in selectedPieces)
            sum += piece.Number;
        return sum;
    }
}
