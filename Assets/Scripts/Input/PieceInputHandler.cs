using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マウスでピースをなぞって合計を判定
/// </summary>
public class PieceInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private SelectionEvaluator evaluator;

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
    }

    void TrySelectPieceAtMouse()
    {
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if (hit != null)
        {
            Piece piece = hit.GetComponent<Piece>();
            if (piece != null && !selectedPieces.Contains(piece))
            {
                if (selectedPieces.Count == 0 || IsAdjacent(selectedPieces[selectedPieces.Count - 1], piece))
                {
                    selectedPieces.Add(piece);
                    piece.SetSelected(true);
                }
            }
        }
    }

    bool IsAdjacent(Piece a, Piece b)
    {
        Vector2 posA = a.transform.position;
        Vector2 posB = b.transform.position;
        return Vector2.Distance(posA, posB) <= 1.1f;
    }

    void ClearSelection()
    {
        foreach (var piece in selectedPieces)
        {
            piece.SetSelected(false);
        }
        selectedPieces.Clear();
    }
}
