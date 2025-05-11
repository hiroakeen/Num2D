using UnityEngine;

/// <summary>
/// 盤面上のマス情報（ピースとのリンク）
/// </summary>
public class Tile
{
    public Vector2Int GridPosition { get; private set; }
    public Piece CurrentPiece { get; private set; }

    public Tile(int x, int y)
    {
        GridPosition = new Vector2Int(x, y);
    }

    public void SetPiece(Piece piece)
    {
        CurrentPiece = piece;
    }

    public void ClearPiece()
    {
        CurrentPiece = null;
    }

    public bool HasPiece()
    {
        return CurrentPiece != null;
    }
}
