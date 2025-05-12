using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 選択中のピースを線でつなぐクラス（LineRenderer制御）
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        // マテリアルや色もここで設定可能（デフォルト線だと黒）
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    /// <summary>
    /// 線を更新（選択中ピースの位置を順に接続）
    /// </summary>
    public void UpdateLine(List<Piece> selectedPieces)
    {
        if (selectedPieces == null || selectedPieces.Count == 0)
        {
            ClearLine();
            return;
        }

        lineRenderer.positionCount = selectedPieces.Count;
        for (int i = 0; i < selectedPieces.Count; i++)
        {
            lineRenderer.SetPosition(i, selectedPieces[i].transform.position);
        }
    }

    /// <summary>
    /// 線を消す
    /// </summary>
    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
