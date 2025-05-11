using UnityEngine;

/// <summary>
/// 盤面の生成・参照・落下処理を管理する
/// </summary>
public class BoardManager : MonoBehaviour
{
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private GameObject piecePrefab;

    private Tile[,] tiles;

    void Start()
    {
        InitBoard();
    }

    void InitBoard()
    {
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(x, y);

                Vector3 spawnPos = new Vector3(x * tileSize, y * tileSize, 0);
                GameObject pieceObj = Instantiate(piecePrefab, spawnPos, Quaternion.identity, transform);

                int number = Random.Range(1, 10);
                Piece piece = pieceObj.GetComponent<Piece>();
                piece.Init(number);

                tiles[x, y].SetPiece(piece);
            }
        }
    }

    public void RemovePiece(Piece piece)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y].HasPiece() && tiles[x, y].CurrentPiece == piece)
                {
                    tiles[x, y].ClearPiece();
                    return;
                }
            }
        }
    }

    public void DropPieces()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1) - 1; y++)
            {
                if (!tiles[x, y].HasPiece())
                {
                    for (int upperY = y + 1; upperY < tiles.GetLength(1); upperY++)
                    {
                        if (tiles[x, upperY].HasPiece())
                        {
                            Piece fallingPiece = tiles[x, upperY].CurrentPiece;

                            tiles[x, y].SetPiece(fallingPiece);
                            tiles[x, upperY].ClearPiece();

                            Vector3 targetPos = new Vector3(x * tileSize, y * tileSize, 0);
                            fallingPiece.AnimateDrop(targetPos); // ← ここがポイント

                            break;
                        }
                    }
                }
            }
        }
    }

    public void RefillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                if (!tiles[x, y].HasPiece())
                {
                    Vector3 spawnPos = new Vector3(x * tileSize, y * tileSize, 0);
                    GameObject pieceObj = Instantiate(piecePrefab, spawnPos, Quaternion.identity, transform);

                    int number = Random.Range(1, 10);
                    Piece newPiece = pieceObj.GetComponent<Piece>();
                    newPiece.Init(number);

                    tiles[x, y].SetPiece(newPiece);
                }
            }
        }
    }

}
